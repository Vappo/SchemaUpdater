using System;
using NUnit.Framework;
using SchemaUpdater.Updates.AddColumn;

namespace SchemaUpdater.Test.Updates
{
    [TestFixture]
    public class AddColumnUpdateFactoryTest
    {
        [Test]
        public void TestCreateAddColumnUpdate()
        {
            var database = new Database("connectionString", "System.Data.SqlClient");

            AddColumnUpdate update = AddColumnUpdateFactory.CreateAddColumnUpdate("Table", database);

            Assert.IsNotNull(update);
        }

        [Test]
        public void TestCreateAddColumnUpdateWithUnknownProvider()
        {
            var database = new Database("connectionString", "System.Data.None");

            Assert.Throws(typeof(ArgumentException), () =>
            {
                AddColumnUpdateFactory.CreateAddColumnUpdate("Table", database);
            });
        }
    }
}