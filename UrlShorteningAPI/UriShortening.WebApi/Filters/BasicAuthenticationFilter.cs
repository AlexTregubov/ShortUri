namespace UriShortening.WebApi.Filters
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Http.Controllers;
    using Autofac.Integration.WebApi;

    internal class BasicAuthenticationFilter : IAutofacAuthorizationFilter
    {
        private const string BasicAuthResponseHeaderValue = "Basic";

        private readonly string Username;

        private readonly string PasswordHash;

        public BasicAuthenticationFilter()
        {
            Username = ConfigurationManager.AppSettings["Username"];
            PasswordHash = ConfigurationManager.AppSettings["PasswordHash"];
        }

        public void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext.ControllerContext.Request))
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);
        }

        protected void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                RequestMessage = actionContext.Request
            };
        }

        private static NetworkCredential ParseAuthorizationHeader(string authHeader)
        {
            string[] credentials;

            try
            {
                credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader)).Split(':');
            }
            catch (FormatException)
            {
                return null;
            }

            if (credentials.Length != 2 ||
                string.IsNullOrEmpty(credentials[0]) ||
                string.IsNullOrEmpty(credentials[1]))
            {
                return null;
            }

            return new NetworkCredential
            {
                UserName = credentials[0],
                Password = credentials[1]
            };
        }

        private bool AuthorizeRequest(HttpRequestMessage request)
        {
            var authValue = request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(authValue?.Parameter) ||
                string.IsNullOrWhiteSpace(authValue.Scheme) ||
                authValue.Scheme != BasicAuthResponseHeaderValue)
            {
                return false;
            }

            var authorizationModel = ParseAuthorizationHeader(authValue.Parameter);

            return authorizationModel != null &&
                   Username.Equals(authorizationModel.UserName) &&
                   PasswordHash.Equals(GetHash(authorizationModel.Password));
        }

        public static string GetHash(string value)
        {
            using (var hash = SHA256.Create())
            {
                return string.Join(string.Empty, hash
                    .ComputeHash(Encoding.UTF8.GetBytes(value))
                    .Select(item => item.ToString("x2")));
            }
        }
    }
}