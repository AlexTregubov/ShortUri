namespace UriShortening.WebApi.Controllers
{
    using System.Web.Http;
    using Routes;

    [AllowAnonymous]
    [RoutePrefix(PingRoutes.BaseRoute)]
    public class PingController : ApiController
    {
        [Route(PingRoutes.PingRoute)]
        [HttpGet]
        public string Ping()
        {
            return "pong";
        }
    }
}