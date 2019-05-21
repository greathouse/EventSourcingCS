using System;
using System.Data.SQLite;
using System.IO;
using ConsoleApp1.Commands;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.Json;
using GreenMoonSoftware.EventSourcing.SqlLite;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new SimpleBus();
            var connectionString = $"Data Source=/var/folders/7z/9yx_mn5n3jv726jj7r0qyzp40000gn/T/tmp76YuZR.tmp;Version=3;";
            var config = new DatabaseConfiguration
            {
                ConnectionString = connectionString,
                TableName = "CustomerEvent"
            };
            
            var customerService = new CustomerService(bus, new CustomerQuery(config), SqliteSubscriber(config));
            
            customerService.Execute(new CreateCustomerCommand(Guid.NewGuid().ToString()) { Username = "kofspades" });
        }

        private static SqlLiteEventSubscriber<IEvent> SqliteSubscriber(DatabaseConfiguration config)
        {
            DropEventTable(config);
            CreateEventTable(config);
            return new SqlLiteEventSubscriber<IEvent>(config, new JsonEventSerializer());;
        }

        private static void DropEventTable(DatabaseConfiguration config)
        {
            var conn = new SQLiteConnection(config.ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"DROP TABLE IF EXISTS {config.TableName}";
            cmd.ExecuteNonQuery();
        }

        private static void CreateEventTable(DatabaseConfiguration configuration)
        {
            var conn = new SQLiteConnection(configuration.ConnectionString);
            conn.Open();
            var createTable = conn.CreateCommand();
            var tableCommandText = $@"create table IF NOT EXISTS {configuration.TableName} (
                                            id TEXT,
                                            aggregateId TEXT,
                                            eventType TEXT,
                                            eventDateTime TEXT,
                                            savedTimestamp TEXT,
                                            data BLOB)";
            createTable.CommandText = tableCommandText;
            createTable.ExecuteNonQuery();
        }
    }
}