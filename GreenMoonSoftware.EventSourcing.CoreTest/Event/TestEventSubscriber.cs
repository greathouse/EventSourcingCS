using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class TestEventSubscriber : IEventSubscriber<IEvent>
    {
        public List<IEvent> Events { get; set; } = new List<IEvent>();

        public void OnEvent(IEvent @event)
        {
            Events.Add(@event);
        }
    }
}