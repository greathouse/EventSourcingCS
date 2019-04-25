using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Core.Store
{
    public abstract class InMemoryStore<T> 
        : IStoreRetrieval<T>, IEventSubscriber<IEvent>
        where T: IAggregate
    {
        private readonly Dictionary<string, EventList> _eventsByAggregate = new Dictionary<string, EventList>();
        
        public abstract T Create();
        
        public T Retrieve(string aggregateId)
        {
            T aggregate = Create();
            aggregate.Apply(EventsForAggregate(aggregateId));
            return aggregate;
        }

        public void OnEvent(IEvent @event)
        {
            var events = EventsForAggregate(@event.AggregateId);
            events.Add(@event);
            _eventsByAggregate[@event.AggregateId] = events;
        }

        private EventList EventsForAggregate(string aggregateId)
        {
            return _eventsByAggregate.GetValueOrDefault(aggregateId, new EventList());
        }
    }
}