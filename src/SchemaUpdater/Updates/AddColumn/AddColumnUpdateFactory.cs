using System;

namespace SchemaUpdater.Updates.AddColumn
{
    public static class AddColumnUpdateFactory
    {
        public static AddColumnUpdate CreateAddColumnUpdate(string tableName, Database database)
        {
            switch (database.Provider)
            {
                case DatabaseProvider.MSSQL: return new SQLAddColumnUpdate(tableName, database);
                case DatabaseProvider.SQLite: return new SQLiteAddColumnUpdate(tableName, database);
            }

            throw new ArgumentException("The given provider is not supported.");
        }
    }
}