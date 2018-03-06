using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchemaUpdater.Types
{
    public class SQLiteDbTypeMap : IDbTypeMap
    {
        private readonly static Dictionary<Type, string> StaticDbTypeMap = new Dictionary<Type, string>()
        {
            [typeof(short)] = "smallint",
            [typeof(int)] = "integer",
            [typeof(long)] = "bigint",
            [typeof(decimal)] = "decimal",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
            [typeof(string)] = "varchar",
            [typeof(char)] = "char",
            [typeof(byte[])] = "blob",
            [typeof(DateTime)] = "datetime"
        };

        public IReadOnlyDictionary<Type, string> DbTypeMap => new ReadOnlyDictionary<Type, string>(StaticDbTypeMap);
    }
}