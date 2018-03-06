namespace SchemaUpdater
{
    public class Column
    {
        public Column(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; }

        public string Type { get; set; }

        public string Length { get; set; }

        public bool IsNullable { get; set; }

        public string DefaultValue { get; set; }

        public string AdditionalInformation { get; set; }
    }
}