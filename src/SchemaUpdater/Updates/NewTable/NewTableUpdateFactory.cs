using System;

namespace SchemaUpdater.Updates.NewTable
{
    public static class NewTableUpdateFactory
    {
        public static NewTableUpdate CreateNewTableUpdate(string tableName, Database database)
        {
            switch (database.Provider)
            {
                case DatabaseProvider.MSSQL: return new SQLNewTableUpdate(tableName, database);
                case DatabaseProvider.SQLite: return new SQLiteNewTableUpdate(tableName, database);
            }

            throw new ArgumentException("The given provider is not supported.");
        }
    }
}