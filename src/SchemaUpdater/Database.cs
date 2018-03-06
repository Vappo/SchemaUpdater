using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SchemaUpdater
{
    public class Database
    {
        private static readonly Dictionary<string, DatabaseProvider> ProviderMapping = new Dictionary<string, DatabaseProvider>
        {
            ["System.Data.SqlClient"] = DatabaseProvider.MSSQL,
            ["System.Data.SQLite"] = DatabaseProvider.SQLite
        };

        public Database(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;

            DetermineProviderFromProviderName();
        }

        public string ConnectionString { get; }

        public string ProviderName { get; }

        public DatabaseVersion Version { get; }

        public DatabaseProvider Provider { get; private set; }

        public IDbConnection CreateConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            return factory.CreateConnection();
        }

        private void DetermineProviderFromProviderName()
        {
            if (ProviderMapping.ContainsKey(ProviderName))
            {
                Provider = ProviderMapping[ProviderName];
            }
            else
            {
                Provider = DatabaseProvider.None;
            }
        }
    }
}