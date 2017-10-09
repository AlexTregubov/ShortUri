namespace UriShortening.BusinessLogic.Helpers
{
    using System;
    using System.Configuration;

    public class ShortUriGenerator
    {
        private static readonly string BaseUiUrl;
        private static readonly int KeyLength;

        static ShortUriGenerator()
        {
            BaseUiUrl = ConfigurationManager.AppSettings["BaseUiUrl"];
            KeyLength = int.Parse(ConfigurationManager.AppSettings["KeyLength"]);
        }

        public static string CreateShortUri()
        {
            var newKey = Guid.NewGuid().ToString("N").ToLower().Substring(0, KeyLength);

            return string.Concat(BaseUiUrl, newKey);
        }
    }
}
