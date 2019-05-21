using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public abstract class SqlLiteAggregateQuery<T> where T: IAggregate
    {
        private readonly DatabaseConfiguration _configuration;

        public SqlLiteAggregateQuery(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract T Create();

        public T Retrieve(string aggregateId)
        {
            return Create();
        }
    }
}