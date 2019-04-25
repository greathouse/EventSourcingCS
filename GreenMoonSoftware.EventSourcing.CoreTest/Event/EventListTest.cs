using System;
using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Event;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class EventListTest
    {
        [Fact]
        public void GivenOneEvent_ShouldExecuteAction()
        {
            var expected = Guid.NewGuid().ToString();
            var actual = "";
            var x = new EventList(new TestEvent(expected));
            x.ForEach(e => actual = e.AggregateId);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenMultipleEvents_ShouldExecutionActionOnAll()
        {
            var first = new TestEvent("One");
            var second = new TestEvent("Two");
            var x = new EventList(first);
            x.Add(second);

            var actuals = new List<IEvent>();
            x.ForEach(e => actuals.Add(e));

            Assert.Contains<IEvent>(first, actuals);
            Assert.Contains<IEvent>(second, actuals);
        }
    }

    class TestEvent : AbstractEvent
    {
        public TestEvent(string id) : base(id, Guid.NewGuid().ToString())
        {
            
        }
    }
}