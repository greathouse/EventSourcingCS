using System;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.Json;
using GreenMoonSoftware.EventSourcing.SqlLite;
using Newtonsoft.Json;
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
        public void GivenObjectSerializer_ShouldSaveEvents()
        {
            Run_ShouldSaveEvents(new ObjectEventSerializer());
        }

        [Fact]
        public void GivenJsonSerializer_ShouldSaveEvents()
        {
            Run_ShouldSaveEvents(new JsonEventSerializer());
        }

        private void Run_ShouldSaveEvents(IEventSerializer eventSerializer)
        {
            var configuration = SetupDatabase();

            var subscriber = new SqlLiteEventSubscriber<IEvent>(configuration, eventSerializer);

            var aggregateId = Guid.NewGuid().ToString();
            subscriber.OnEvent(new TestEvent1(aggregateId));
            subscriber.OnEvent(new TestEvent2(aggregateId));

            var connection = configuration.CreateConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"select count(*) from {configuration.TableName}";

            var actual = command.ExecuteScalar();

            Assert.Equal(2L, actual);
        }

        [Fact]
        public void GivenObjectSerializer_ShouldWriteEventData()
        {
            Run_ShouldWriteEventData(new ObjectEventSerializer());
        }

        [Fact]
        public void GivenJsonSerializer_ShouldWriteEventData()
        {
            Run_ShouldWriteEventData(new JsonEventSerializer());
        }

        private void Run_ShouldWriteEventData(IEventSerializer serializer)
        {
            var configuration = SetupDatabase();

            var subscriber = new SqlLiteEventSubscriber<IEvent>(configuration, serializer);

            var aggregateId = Guid.NewGuid().ToString();
            var expectedData = $"This is my random data {Guid.NewGuid()}";
            var testEvent = new EventWithData(aggregateId, expectedData);
            subscriber.OnEvent(testEvent);

            var connection = configuration.CreateConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"select data from {configuration.TableName}";

            var actualBytes = command.ExecuteScalar();
            var actual = serializer.Deserialize(testEvent.Type, (byte[]) actualBytes);
            Assert.IsType<EventWithData>(actual);
            Assert.Equal(expectedData, ((EventWithData) actual).Data);
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