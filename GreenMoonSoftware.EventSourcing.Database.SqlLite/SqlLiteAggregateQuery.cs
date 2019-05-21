using System.Data.SQLite;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public abstract class SqlLiteAggregateQuery<T> where T: IAggregate
    {
        private readonly DatabaseConfiguration _configuration;
        private readonly IEventSerializer _serializer;

        public SqlLiteAggregateQuery(DatabaseConfiguration configuration, IEventSerializer serializer)
        {
            _configuration = configuration;
            _serializer = serializer;
        }

        public abstract T Create();

        public T Retrieve(string aggregateId)
        {
            var x = Create();
            using (var conn = new SQLiteConnection(_configuration.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from {_configuration.TableName} where aggregateId = @aggregateId order by eventDateTime asc";
                cmd.Parameters.AddWithValue("@aggregateId", aggregateId);
                var reader = cmd.ExecuteReader();
                var events = new EventList();
                while (reader.Read())
                {
                    events.Add(_serializer.Deserialize((string)reader["eventType"], (byte[])reader["Data"]));
                }
                x.Apply(events);
            }
            return x;
        }
    }
}