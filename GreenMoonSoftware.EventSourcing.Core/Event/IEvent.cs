using System;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface IEvent
    {
        Guid Id { get; }
        string AggregateId { get; }
        string Type { get; }
        DateTime EventDateTime { get; }
    }
}