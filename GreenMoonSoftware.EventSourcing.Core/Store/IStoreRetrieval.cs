using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Core.Store
{
    public interface IStoreRetrieval<T> where T: IAggregate
    {
        T Retrieve(string aggregateId);
    }
}