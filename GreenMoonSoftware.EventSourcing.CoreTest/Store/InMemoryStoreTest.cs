using System;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Core.Store;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Store
{
    public class InMemoryStoreTest
    {
        [Fact]
        public void WhenEventsAreStored_ShouldReturnPopulatedAggregate()
        {
            var aggregateId = Guid.NewGuid().ToString();
            var store = new TestInMemoryStore();
            store.OnEvent(new Event1(aggregateId));

            var actual = store.Retrieve(aggregateId);
            
            Assert.Equal(1, actual.Event1Handled);
            Assert.Equal(0, actual.Event2Handled);

            store.OnEvent(new Event2(aggregateId));

            actual = store.Retrieve(aggregateId);
            
            Assert.Equal(1, actual.Event1Handled);
            Assert.Equal(1, actual.Event2Handled);
            
            store.OnEvent(new Event2(aggregateId));
            actual = store.Retrieve(aggregateId);
            Assert.Equal(2, actual.Event2Handled);
        }
    }

    public class TestInMemoryStore : InMemoryStore<TestAggregate>
    {
        public override TestAggregate Create()
        {
            return new TestAggregate();
        }
    }

    public class TestAggregate : IAggregate
    {
        public string Id { get; }

        public int Event1Handled { get; private set; } = 0;
        public int Event2Handled { get; private set; } = 0;
        
        public void Apply(EventList events)
        {
            events.ForEach(x => EventApplier.Apply(this, x));
        }

        public void Handle(Event1 e)
        {
            Event1Handled++;
        }

        public void Handle(Event2 e)
        {
            Event2Handled++;
        }
    }

    public class Event1 : AbstractEvent
    {
        public Event1(string aggregateId) : base(aggregateId, typeof(Event1).Name)
        {
        }
    }

    public class Event2 : AbstractEvent
    {
        public Event2(string aggregateId) : base(aggregateId, typeof(Event2).Name)
        {
        }
    }
}