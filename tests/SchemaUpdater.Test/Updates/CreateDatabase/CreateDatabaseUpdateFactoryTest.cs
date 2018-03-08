using NUnit.Framework;
using SchemaUpdater.Exceptions;
using SchemaUpdater.Updates.CreateDatabase;

namespace SchemaUpdater.Test.Updates
{
    [TestFixture]
    public class CreateDatabaseUpdateFactoryTest
    {
        [Test]
        public void TestCreateDatabaseUpdate()
        {
            var database = new Database("connectionString", "System.Data.SqlClient");

            CreateDatabaseUpdate update = CreateDatabaseUpdateFactory.CreateDatabaseUpdate(database);

            Assert.IsNotNull(update);
        }

        [Test]
        public void TestCreateDatabaseUpdateWithUnknownProvider()
        {
            var database = new Database("connectionString", "System.Data.None");

            Assert.Throws(typeof(SchemaUpdaterException), () =>
            {
                CreateDatabaseUpdateFactory.CreateDatabaseUpdate(database);
            });
        }
    }
}