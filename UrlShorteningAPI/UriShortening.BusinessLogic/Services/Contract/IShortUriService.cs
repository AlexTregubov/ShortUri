namespace UriShortening.BusinessLogic.Services.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IShortUriService
    {
        Task<UriModel> CreateShortUrl(AddUriModel input);

        Task<List<UriModel>> GetUriByFilter(UriFilter filter);
    }
}
