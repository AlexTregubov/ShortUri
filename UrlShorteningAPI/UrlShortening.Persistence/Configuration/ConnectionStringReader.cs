namespace UriShortening.Persistence.Configuration
{
    using System;
    using System.Configuration;

    public static class ConnectionStringReader
    {
        public static string GetConnectionString(string contextName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[contextName];
            if (connectionStringSettings == null)
            {
                throw new InvalidOperationException($"Unable to find connectionString for {contextName}");
            }

            return connectionStringSettings.ConnectionString;
        }
    }
}
