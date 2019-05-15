using System;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.SqlLite;
using Xunit;
using Xunit.Abstractions;

namespace GreenMoonSoftware.EventSourcing.SqlLiteTest
{
    public class SqlLiteEventSubscriberTest
    {
        private readonly ITestOutputHelper output;
        
        public SqlLiteEventSubscriberTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void ShouldSaveEvents()
        {
            var configuration = SetupDatabase();

            var subscriber = new SqlLiteEventSubscriber<IEvent>(configuration, new ObjectEventSerializer());

            var aggregateId = Guid.NewGuid().ToString();
            subscriber.OnEvent(new TestEvent1(aggregateId));
            subscriber.OnEvent(new TestEvent2(aggregateId));
            
            var connection = new SQLiteConnection(configuration.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"select count(*) from {configuration.TableName}";

            var actual = command.ExecuteScalar();
            
            Assert.Equal(2L, actual);
        }

        [Fact]
        public void ShouldWriteEventData()
        {
            var configuration = SetupDatabase();

            var serializer = new ObjectEventSerializer();
            var subscriber = new SqlLiteEventSubscriber<IEvent>(configuration, serializer);

            var aggregateId = Guid.NewGuid().ToString();
            var expectedData = $"This is my random data {Guid.NewGuid()}";
            subscriber.OnEvent(new EventWithData(aggregateId, expectedData));
            
            var connection = new SQLiteConnection(configuration.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"select data from {configuration.TableName}";

            var actualBytes = command.ExecuteScalar();
            var actual = serializer.Deserialize("", (byte[])actualBytes);
            Assert.IsType<EventWithData>(actual);
            Assert.Equal(expectedData, ((EventWithData)actual).Data);
        }

        private DatabaseConfiguration SetupDatabase()
        {
            var configuration = TestHelper.Configuration();
            output.WriteLine(configuration.ConnectionString);
            TestHelper.CreateEventTable(configuration);
            return configuration;
        }
    }

    [Serializable]
    public class TestEvent1 : AbstractEvent
    {
        public TestEvent1(string aggregateId) : base(aggregateId, typeof(TestEvent1).FullName)
        {
        }
    }

    [Serializable]
    public class TestEvent2 : AbstractEvent
    {
        public TestEvent2(string aggregateId) : base(aggregateId, typeof(TestEvent2).FullName)
        {
        }
    }

    [Serializable]
    public class EventWithData : AbstractEvent
    {
        public string Data { get; set; }


        public EventWithData(string aggregateId, string data) : base(aggregateId, typeof(EventWithData).FullName)
        {
            this.Data = data;
        }
    }
}