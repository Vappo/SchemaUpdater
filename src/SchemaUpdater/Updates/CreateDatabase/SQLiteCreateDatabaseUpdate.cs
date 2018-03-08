using System.Data;

namespace SchemaUpdater.Updates.CreateDatabase
{
    public class SQLiteCreateDatabaseUpdate : CreateDatabaseUpdate
    {
        public SQLiteCreateDatabaseUpdate(Database database) : base(database)
        {
        }

        public override void Update(string connectionString)
        {
            using (IDbConnection connection = Database.CreateConnection())
            {
                connection.Open();
            }
        }
    }
}