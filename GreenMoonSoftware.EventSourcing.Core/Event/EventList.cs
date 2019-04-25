using System;
using System.Collections.Generic;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class EventList
    {
        private readonly List<IEvent> events = new List<IEvent>();

        public EventList(IEvent e)
        {
            events.Add(e);
        }

        public EventList Add(IEvent e)
        {
            events.Add(e);
            return this;
        }

        public EventList AddAll(IEnumerable<IEvent> events)
        {
            this.events.AddRange(events);
            return this;
        }

        public void ForEach(Action<IEvent> action)
        {
            events.ForEach(action.Invoke);
        }
    }
}