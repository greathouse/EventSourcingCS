using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.SqlLite;

namespace ConsoleApp1
{
    public class CustomerQuery : SqlLiteAggregateQuery<CustomerAggregate>
    {
        public CustomerQuery(DatabaseConfiguration configuration, IEventSerializer serializer) : base(configuration, serializer)
        {
        }

        public override CustomerAggregate Create()
        {
            return new CustomerAggregate();
        }
    }
}