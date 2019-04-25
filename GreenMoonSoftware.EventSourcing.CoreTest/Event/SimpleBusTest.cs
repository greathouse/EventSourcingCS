using GreenMoonSoftware.EventSourcing.Core.Event;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class SimpleBusTest
    {
        [Fact]
        public void GivenSubscriber_WhenPost_ShouldSendEventToSubscriber()
        {
            var @event = new TestEvent("One");
            var subscriber = new TestEventSubscriber();
            
            var bus = new SimpleBus();
            bus.Register(subscriber);
            bus.Post(@event);
            
            Assert.Single(subscriber.Events);
        }

        [Fact]
        public void GivenMultipleSubscribers_WhenPost_ShouldSentEventToAllSubscribers()
        {
            var @event = new TestEvent("One");
            var subscriber1 = new TestEventSubscriber();
            var subscriber2 = new TestEventSubscriber();
            
            var bus = new SimpleBus();
            bus.Register(subscriber1)
                .Register(subscriber2);
            bus.Post(@event);
            
            Assert.Single(subscriber1.Events);
            Assert.Single(subscriber2.Events);
        }

        [Fact]
        public void GivenSubscriber_WhenUnregister_ShouldNoLongerReceiveEvents()
        {
            var @event = new TestEvent("One");
            var subscriber = new TestEventSubscriber();
            
            var bus = new SimpleBus();
            bus.Register(subscriber);
            bus.Post(@event);
            
            Assert.Single(subscriber.Events);
            
            bus.Unregister(subscriber);
            bus.Post(@event);
            
            Assert.Single(subscriber.Events);
        }
    }
}