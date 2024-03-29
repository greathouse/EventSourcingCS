using System;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public class DatabaseEventSubscriber<T> : IEventSubscriber<T> where T : IEvent
    {
        private readonly DatabaseConfiguration _configuration;
        private readonly IEventSerializer _serializer;

        public DatabaseEventSubscriber(DatabaseConfiguration configuration, IEventSerializer serializer)
        {
            _configuration = configuration;
            _serializer = serializer;
        }

        public void OnEvent(T @event)
        {
            using (var conn = _configuration.CreateConnection())
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
                cmd.AddWithValue("@id", @event.Id);
                cmd.AddWithValue("@aggregateId", @event.AggregateId);
                cmd.AddWithValue("@eventType", @event.Type);
                cmd.AddWithValue("@eventDateTime", @event.EventDateTime);
                cmd.AddWithValue("@savedTimestamp", DateTime.Now);
                cmd.AddWithValue("@data", _serializer.Serialize(@event));
                cmd.ExecuteNonQuery();
            }
        }
    }
}