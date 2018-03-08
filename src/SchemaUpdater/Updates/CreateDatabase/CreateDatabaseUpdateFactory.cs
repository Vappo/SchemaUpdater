using SchemaUpdater.Exceptions;

namespace SchemaUpdater.Updates.CreateDatabase
{
    public static class CreateDatabaseUpdateFactory
    {
        public static CreateDatabaseUpdate CreateDatabaseUpdate(Database database)
        {
            switch (database.Provider)
            {
                case DatabaseProvider.MSSQL: return new SQLCreateDatabaseUpdate(database);
                case DatabaseProvider.SQLite: return new SQLiteCreateDatabaseUpdate(database);
            }

            throw new SchemaUpdaterException(ErrorCodes.ProviderNotSupported);
        }
    }
}