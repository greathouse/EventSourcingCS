using System;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public abstract class AbstractEvent : IEvent
    {
        public AbstractEvent(string aggregateId, string type) {
            this.AggregateId = aggregateId;
            this.Type = type;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public string AggregateId { get; }

        public string Type { get; }

        public DateTime EventDateTime { get; } = DateTime.Now;
    }
}