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
            var configuration = new DatabaseConfiguration
            {
                ConnectionString = $"Data Source={Path.GetTempFileName()};Version=3;",
                TableName = "TestAggregate"
            };
            CreateEventTable(configuration);
            
            var query = new SqlLiteAggregateQuery<TestAggregate>(configuration);
            var aggregateId = Guid.NewGuid().ToString();
            query.Retrieve(aggregateId);
        }

        private static void CreateEventTable(DatabaseConfiguration configuration)
        {
            var conn = new SQLiteConnection(configuration.ConnectionString);
            conn.Open();
            var createTable = conn.CreateCommand();
            createTable.CommandText = @"create table 
                                            id VARCHAR,
                                            aggregateId VARCHAR,
                                            eventType VARCHAR,
                                            eventDateTime TIMESTAMP,
                                            savedTimestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP(),
                                            data BLOB";
            createTable.ExecuteNonQuery();
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