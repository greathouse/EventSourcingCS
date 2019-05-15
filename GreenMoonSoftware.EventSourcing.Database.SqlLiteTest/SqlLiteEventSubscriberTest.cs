using System;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.SqlLite;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.SqlLiteTest
{
    public class SqlLiteEventSubscriberTest
    {
        [Fact]
        public void ShouldSaveEvents()
        {
            var configuration = TestHelper.Configuration();
            TestHelper.CreateEventTable(configuration);

            var subscriber = new SqlLiteEventSubscriber<IEvent>(configuration);

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
}