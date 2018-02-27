using System;
using System.Collections.Generic;
using SchemaUpdater.Types;

namespace SchemaUpdater.Updates
{
    public abstract class AddFieldUpdate : BaseTableUpdate
    {
        private List<Column> _columns = new List<Column>();

        public AddFieldUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        public virtual void AddColumn<T>(
            string columnName,
            string length = "0",
            bool isNullable = false,
            string defaultValue = "",
            string additionalInformation = "")
        {
            string type = GetDbTypeFromFrameworkType<T>();
            AddColumn(columnName, type, length, isNullable, defaultValue, additionalInformation);
        }

        public virtual void AddColumn(
            string columnName,
            string type,
            string length = "0",
            bool isNullable = false,
            string defaultValue = "",
            string additionalInformation = "")
        {
            var column = new Column(columnName);
            column.AdditionalInformation = additionalInformation;
            column.DefaultValue = defaultValue;
            column.IsNullable = isNullable;
            column.Length = length;
            column.Type = type;

            AddColumn(column);
        }

        public virtual void AddColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }

            _columns.Add(column);
        }

        private string GetDbTypeFromFrameworkType<T>()
        {
            IDbTypeMap map = DbTypeMapFactory.CreateDbTypeMap(Database.Provider);
            Type type = typeof(T);
            return map.DbTypeMap[type];
        }
    }
}