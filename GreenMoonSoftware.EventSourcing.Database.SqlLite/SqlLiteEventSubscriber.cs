using System;
using System.Data;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public class SqlLiteEventSubscriber<T> : IEventSubscriber<T> where T : IEvent
    {
        private readonly DatabaseConfiguration _configuration;

        public SqlLiteEventSubscriber(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
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
                                    ) values (
                                        @id
                                        , @aggregateId
                                        , @eventType
                                        , @eventDateTime
                                        , @savedTimestamp
                                    )";
                cmd.Parameters.AddWithValue("@id", @event.Id);
                cmd.Parameters.AddWithValue("@aggregateId", @event.AggregateId);
                cmd.Parameters.AddWithValue("@eventType", @event.Type);
                cmd.Parameters.AddWithValue("@eventDateTime", @event.EventDateTime);
                cmd.Parameters.AddWithValue("@savedTimestamp", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }
}