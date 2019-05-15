using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public class SqlLiteAggregateQuery<T> where T: IAggregate
    {
        private readonly DatabaseConfiguration _configuration;

        public SqlLiteAggregateQuery(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Retrieve(string aggregateId)
        {
            return default(T);
        }
    }
}