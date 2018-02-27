using System.Data;
using System.Data.Common;

namespace SchemaUpdater
{
    public class Database
    {
        public Database(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        public string ConnectionString { get; }

        public string ProviderName { get; }

        public DatabaseVersion Version { get; }

        public DatabaseProvider Provider { get; }

        public IDbConnection CreateConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            return factory.CreateConnection();
        }
    }
}