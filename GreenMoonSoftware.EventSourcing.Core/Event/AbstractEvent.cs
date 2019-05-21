using System;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    [Serializable]
    public abstract class AbstractEvent : IEvent
    {
        public AbstractEvent(string aggregateId, string type)
        {
            AggregateId = aggregateId;
            Type = type;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public string AggregateId { get; }

        public string Type { get; }

        public DateTime EventDateTime { get; } = DateTime.Now;
    }
}