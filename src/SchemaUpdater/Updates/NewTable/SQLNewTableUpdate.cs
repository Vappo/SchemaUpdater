using System.Text;

namespace SchemaUpdater.Updates.NewTable
{
    public class SQLNewTableUpdate : NewTableUpdate
    {
        private StringBuilder _commandBuilder = new StringBuilder();

        public SQLNewTableUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected override string CreateCommandText()
        {
            _commandBuilder.Clear();

            AddTableExistsCheckToBuilder();
            AddCreateTableCommandToBuilder();
            AddTableColumnsToBuilder();
            AddPrimaryKeyToBuilder();

            return _commandBuilder.ToString();
        }

        private void AddTableExistsCheckToBuilder()
        {
            _commandBuilder.Append($" IF NOT EXISTS (SELECT * FROM sysobjects WHERE Name='{TableName}' and Xtype='U') ");
        }

        private void AddCreateTableCommandToBuilder()
        {
            _commandBuilder.Append($" CREATE TABLE {TableName} ( ");
        }

        private void AddTableColumnsToBuilder()
        {
            foreach (var item in Columns)
            {
            }
        }

        private void AddPrimaryKeyToBuilder()
        {

        }
    }
}