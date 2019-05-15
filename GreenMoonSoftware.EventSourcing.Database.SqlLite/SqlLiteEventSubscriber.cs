using System;
using System.Data.SQLite;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public class SqlLiteEventSubscriber<T> : IEventSubscriber<T> where T : IEvent
    {
        private readonly DatabaseConfiguration _configuration;
        private readonly IEventSerializer _serializer;

        public SqlLiteEventSubscriber(DatabaseConfiguration configuration, IEventSerializer serializer)
        {
            _configuration = configuration;
            _serializer = serializer;
        }

        public void OnEvent(T @event)
        {
            using (var conn = new SQLiteConnection(_configuration.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $@"insert into {_configuration.TableName} (
                                            id
                                            , aggregateId
                                            , eventType
                                            , eventDateTime
                                            , savedTimestamp
                                            , data
                                    ) values (
                                        @id
                                        , @aggregateId
                                        , @eventType
                                        , @eventDateTime
                                        , @savedTimestamp
                                        , @data
                                    )";
                cmd.Parameters.AddWithValue("@id", @event.Id);
                cmd.Parameters.AddWithValue("@aggregateId", @event.AggregateId);
                cmd.Parameters.AddWithValue("@eventType", @event.Type);
                cmd.Parameters.AddWithValue("@eventDateTime", @event.EventDateTime);
                cmd.Parameters.AddWithValue("@savedTimestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@data", _serializer.Serialize(@event));
                cmd.ExecuteNonQuery();
            }
        }
    }
}