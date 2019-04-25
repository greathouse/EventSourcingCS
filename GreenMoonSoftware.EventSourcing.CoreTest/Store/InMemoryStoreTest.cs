using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Core.Store;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Store
{
    public class InMemoryStoreTest
    {
        
    }

    public class TestInMemoryStore : InMemoryStore<TestAggregate>
    {
        public override TestAggregate Create()
        {
            return new TestAggregate();
        }
    }

    public class TestAggregate : IAggregate
    {
        public string Id { get; }
        public void Apply(EventList events)
        {
            throw new System.NotImplementedException();
        }
    }
}