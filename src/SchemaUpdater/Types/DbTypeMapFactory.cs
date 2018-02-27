using System;

namespace SchemaUpdater.Types
{
    public static class DbTypeMapFactory
    {
        public static IDbTypeMap CreateDbTypeMap(DatabaseProvider provider)
        {
            switch (provider)
            {
                case DatabaseProvider.MSSQL: return new SqlDbTypeMap();
            }

            throw new ArgumentException("The given provider is not supported.");
        }
    }
}