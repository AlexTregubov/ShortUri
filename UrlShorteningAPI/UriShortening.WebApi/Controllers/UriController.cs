namespace UriShortening.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLogic.Models;
    using BusinessLogic.Services.Contract;
    using Routes;

    [RoutePrefix(UriRoutes.BaseRoute)]
    public class UriController : ApiController
    {
        private readonly IShortUriService uriService;

        public UriController(IShortUriService uriService)
        {
            this.uriService = uriService;
        }

        [Route(UriRoutes.AddUriRoute)]
        [HttpPost]
        public Task<UriModel> CreateShortUri(AddUriModel model)
        {
            return uriService.CreateShortUrl(model);
        }

        [Route(UriRoutes.GetUriRoute)]
        [HttpGet]
        public Task<List<UriModel>> CreateShortUri([FromUri] UriFilter filter)
        {
            return uriService.GetUriByFilter(filter);
        }
    }
}
