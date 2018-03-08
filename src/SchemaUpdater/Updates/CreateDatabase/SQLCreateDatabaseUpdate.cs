using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SchemaUpdater.Updates.CreateDatabase
{
    public class SQLCreateDatabaseUpdate : CreateDatabaseUpdate
    {
        public SQLCreateDatabaseUpdate(Database database) : base(database)
        {
        }

        public override void Update(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            using (IDbConnection connection = CreateConnectionWithoutDatabase(builder))
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = CreateCommandText(builder.InitialCatalog);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private IDbConnection CreateConnectionWithoutDatabase(SqlConnectionStringBuilder builder)
        {
            builder.InitialCatalog = string.Empty;
            return new SqlConnection(builder.ToString());
        }

        private string CreateCommandText(string databaseName)
        {
            var builder = new StringBuilder();
            builder.Append($" CREATE DATABASE {databaseName} ");
            return builder.ToString();
        }
    }
}