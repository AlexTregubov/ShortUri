namespace UriShortening.BusinessLogic.Services
{
    using Validation;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Enums;
    using ErrorHandling;
    using Models;
    using Contract;
    using Persistence.Context;
    using Persistence.Context.Models;

    public class ShortUriService : IShortUriService
    {
        private readonly ShortUriContext dbContext;

        private readonly IValidator validator;

        private readonly IMapper mapper;

        public ShortUriService(
            ShortUriContext dbContext,
            IValidator validator,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<UriModel> CreateShortUrl(AddUriModel input)
        {
            Error.IfNull(input, ErrorCode.InvalidInput, "Invalid input");

            validator.Validate(input);

            var dbShortedUri = await dbContext.ShortedUrls.FirstOrDefaultAsync(x => x.SourceUri == input.Uri);

            if (dbShortedUri == null)
            {
                dbShortedUri = mapper.Map<ShortedUrl>(input);

                dbContext.ShortedUrls.Add(dbShortedUri);
                await dbContext.SaveChangesAsync();
            }

            return mapper.Map<UriModel>(dbShortedUri);
        }


        public async Task<List<UriModel>> GetUriByFilter(UriFilter filter)
        {
            Error.IfNull(filter, ErrorCode.InvalidInput, "Invalid input");

            validator.Validate(filter);

            var dbShortedUris = await dbContext.ShortedUrls.Where(x => x.CreatedById == filter.CreatedById).ToListAsync();

            return mapper.Map<List<UriModel>>(dbShortedUris);
        }
    }
}
