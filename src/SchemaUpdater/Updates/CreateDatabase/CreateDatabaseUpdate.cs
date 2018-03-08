using System;
using System.Data;

namespace SchemaUpdater.Updates.CreateDatabase
{
    public abstract class CreateDatabaseUpdate : BaseTableUpdate
    {
        public CreateDatabaseUpdate(Database database) : base(string.Empty, database)
        {
        }

        public override sealed void Update(IDbConnection connection, IDbTransaction transaction)
        {
            throw new NotSupportedException("Cannot create database with transaction, use Update(string connectionString) instead.");
        }
    }
}