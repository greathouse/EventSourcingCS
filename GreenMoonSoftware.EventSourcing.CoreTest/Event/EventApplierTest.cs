using System;
using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Command;
using GreenMoonSoftware.EventSourcing.Core.Event;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class EventApplierTest
    {
        [Fact]
        public void GivenAggregateWithHandleMethod_ShouldCallDefaultHandleMethod()
        {
            var aggregate = new TestAggregate();
            
            EventApplier.Apply(aggregate, new TestEvent1());
            EventApplier.Apply(aggregate, new TestEvent2());
            
            Assert.True(aggregate.Event1Handled);
            Assert.True(aggregate.Event2Handled);
        }
    }
    
    public class TestAggregate : IAggregate
    {
        public bool Event1Handled { get; private set; }

        public bool Event2Handled { get; private set; }

        public string Id { get; }

        public IEnumerable<IEvent> Handle(TestEvent1 event1)
        {
            Event1Handled = true;
            return new List<IEvent>();
        }

        public IEnumerable<IEvent> Handle(TestEvent2 command)
        {
            Event2Handled = true;
            return new List<IEvent>();
        }
        
        public void Apply(EventList events)
        {
            
        }
    }

    public class TestEvent1 : IEvent
    {
        public Guid Id { get; }
        public string AggregateId { get; }
        public string Type { get; }
        public DateTime EventDateTime { get; }
    }

    public class TestEvent2 : IEvent
    {
        public Guid Id { get; }
        public string AggregateId { get; }
        public string Type { get; }
        public DateTime EventDateTime { get; }
    }
}