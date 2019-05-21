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