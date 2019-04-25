using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Core.Store
{
    public abstract class InMemoryStore<T> 
        : IStoreRetrieval<T>, IEventSubscriber<IEvent>
        where T: IAggregate
    {
        public abstract T Create();
        
        public T Retrieve(string aggregateId)
        {
            return (T) (IAggregate) null;
        }

        public void OnEvent(IEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}