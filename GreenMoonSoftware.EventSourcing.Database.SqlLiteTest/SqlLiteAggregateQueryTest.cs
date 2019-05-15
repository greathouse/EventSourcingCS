using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.SqlLite;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.SqlLiteTest
{
    public class SqlLiteAggregateQueryTest
    {
        [Fact]
        public void Test1()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "CREATE table blah (id varchar(150))";
            command.ExecuteNonQuery();
            command.CommandText = "insert into blah values ('1')";
            command.ExecuteNonQuery();
            command.CommandText = "select * from blah";
            var r = command.ExecuteReader();
            while (r.Read())
            {
                Console.WriteLine(r.GetString(0));
                var blob = r.GetBlob(1, true);
            }
        }

        [Fact]
        public void ShouldStoreAndRetrieveAggregateEvents()
        {
            var configuration = TestHelper.Configuration();
            TestHelper.CreateEventTable(configuration);
            
            var query = new SqlLiteAggregateQuery<TestAggregate>(configuration);
            var aggregateId = Guid.NewGuid().ToString();
            query.Retrieve(aggregateId);
        }
    }

    public class TestAggregate : IAggregate
    {
        public string Id { get; }
        
        public void Apply(EventList events)
        {
            throw new NotImplementedException();
        }
    }
}