using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SchemaUpdater.Exceptions;

namespace SchemaUpdater.Updates.NewTable
{
    public abstract class NewTableUpdate : BaseTableUpdate
    {
        private List<Column> _columns = new List<Column>();
        private PrimaryKey _primaryKey;

        public NewTableUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected List<Column> Columns
        {
            get { return _columns; }
        }

        protected PrimaryKey PrimaryKey
        {
            get { return _primaryKey; }
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

            CheckIfUpdateIsAlreadyLocked();

            _columns.Add(column);
        }

        public void AddPrimaryKey(params string[] primaryKeyColumns)
        {
            if (primaryKeyColumns == null || primaryKeyColumns.Length == 0)
            {
                throw new ArgumentNullException(nameof(primaryKeyColumns));
            }

            var primaryKey = new PrimaryKey(primaryKeyColumns);
            AddPrimaryKey(primaryKey);
        }

        public void AddPrimaryKey(PrimaryKey primaryKey)
        {
            if (primaryKey == null)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }

            CheckIfUpdateIsAlreadyLocked();

            _primaryKey = primaryKey;
        }

        public override void Update(IDbConnection connection, IDbTransaction transaction)
        {
            CheckIfColumnsAreAdded();
            CheckIfPrimaryKeyIsAdded();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandTimeout = CommandTimeout;
                command.CommandText = CreateCommandText();

                if (!string.IsNullOrWhiteSpace(command.CommandText))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CheckIfColumnsAreAdded()
        {
            if (_columns == null || !_columns.Any())
            {
                throw new SchemaUpdaterException(ErrorCodes.NotColumnsAddedToUpdate);
            }
        }

        private void CheckIfPrimaryKeyIsAdded()
        {
            if (_primaryKey == null)
            {
                throw new SchemaUpdaterException(ErrorCodes.PrimaryKeyIsMissing);
            }
        }

        protected abstract string CreateCommandText();
    }
}