using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.SqlLite;

namespace ConsoleApp1
{
    public class CustomerQuery : SqlLiteAggregateQuery<CustomerAggregate>
    {
        public CustomerQuery(DatabaseConfiguration configuration) : base(configuration)
        {
        }

        public override CustomerAggregate Create()
        {
            return new CustomerAggregate();
        }
    }
}