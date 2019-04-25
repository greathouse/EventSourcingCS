using System;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class SimpleEvent : IEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public string Type { get; set; }
        public DateTime EventDateTime { get; set; }
    }
}