using System.Configuration;
using Microsoft.Owin.Cors;

namespace UriShortening.WebApi
{
    using System.Threading.Tasks;
    using System.Web.Cors;
    using Owin;

    public static class CorsExtensions
    {
        public static IAppBuilder UseCors(this IAppBuilder app)
        {
            var policy = new CorsPolicy();

            AllowOrigins(policy, ConfigurationManager.AppSettings["CorsAllowedOrigins"]);
            AllowMethods(policy, ConfigurationManager.AppSettings["CorsAllowedMethods"]);
            AllowHeaders(policy, ConfigurationManager.AppSettings["CorsAllowedHeaders"]);

            var options = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            };

            app.UseCors(options);

            return app;
        }

        private static void AllowOrigins(CorsPolicy policy, string origins)
        {
            if (string.IsNullOrWhiteSpace(origins) || origins == "*")
            {
                policy.AllowAnyOrigin = true;
                return;
            }

            foreach (var origin in origins.Split(';'))
            {
                policy.Origins.Add(origin);
            }
        }

        private static void AllowMethods(CorsPolicy policy, string methods)
        {
            if (string.IsNullOrWhiteSpace(methods) || methods == "*")
            {
                policy.AllowAnyMethod = true;
                return;
            }

            foreach (var method in methods.Split(';'))
            {
                policy.Methods.Add(method);
            }
        }

        private static void AllowHeaders(CorsPolicy policy, string headers)
        {
            if (string.IsNullOrWhiteSpace(headers) || headers == "*")
            {
                policy.AllowAnyHeader = true;
                return;
            }

            foreach (var header in headers.Split(';'))
            {
                policy.Headers.Add(header);
            }
        }

        private static void ExposeHeaders(CorsPolicy policy, string headers)
        {
            if (string.IsNullOrWhiteSpace(headers))
            {
                return;
            }

            foreach (var header in headers.Split(';'))
            {
                policy.ExposedHeaders.Add(header);
            }
        }
    }
}