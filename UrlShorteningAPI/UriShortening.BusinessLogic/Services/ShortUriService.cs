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

            var dbShortedUri = await dbContext.ShortedUrls.FirstOrDefaultAsync(x => x.SourceUri == input.Uri && x.CreatedById == input.CreatedById);

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

        public async Task<UriModel> GetUriByKey(string key)
        {
            var dbShortedUri = await GetGbModelByKey(key);

            return dbShortedUri == null ? null : mapper.Map<UriModel>(dbShortedUri);
        }

        public async Task UpdateUriTransferCount(string key)
        {
            var dbShortedUri = await GetGbModelByKey(key);

            dbShortedUri.TransferCount = dbShortedUri.TransferCount.GetValueOrDefault() + 1;

            await dbContext.SaveChangesAsync();
        }

        private Task<ShortedUrl> GetGbModelByKey(string key)
        {
            Error.IfNull(key, ErrorCode.InvalidInput, "Invalid input");

            return dbContext.ShortedUrls.FirstOrDefaultAsync(x => x.ShortUri == key);
        }
    }
}
