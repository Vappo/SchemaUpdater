﻿using SchemaUpdater.Exceptions;

namespace SchemaUpdater.Types
{
    public static class DbTypeMapFactory
    {
        public static IDbTypeMap CreateDbTypeMap(DatabaseProvider provider)
        {
            switch (provider)
            {
                case DatabaseProvider.MSSQL: return new SQLDbTypeMap();
                case DatabaseProvider.SQLite: return new SQLiteDbTypeMap();
            }

            throw new SchemaUpdaterException(ErrorCodes.ProviderNotSupported);
        }
    }
}