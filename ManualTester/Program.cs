using System;
using System.Data.SQLite;
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
            var program = new Program();
            var command = "";
            while (command != "exit")
            {
                command = Console.ReadLine();
                if (command == null)
                    continue;

                var inputs = command.Split();
                if (inputs[0] == "create")
                    program.CreateCustomer(inputs[1]);

                if (inputs[0] == "update")
                    program.UpdateCustomer(inputs[1], inputs[2], inputs[3]);
            }
            Console.WriteLine("Done!");
        }

        private readonly CustomerService _customerService;

        public Program()
        {
            var bus = new SimpleBus();
            var connectionString = $"Data Source=/var/folders/7z/9yx_mn5n3jv726jj7r0qyzp40000gn/T/tmp76YuZR.tmp;Version=3;";
            var configuration = new DatabaseConfiguration
            {
                ConnectionString = connectionString,
                TableName = "CustomerEvent"
            };
            var query = new CustomerQuery(configuration, new JsonEventSerializer());
            _customerService = new CustomerService(bus, query, SqliteSubscriber(configuration));
            bus.Register(new CustomerReadModelEventSubscriber(configuration));
        }

        private void CreateCustomer(string username)
        {
            _customerService.Execute(new CreateCustomerCommand(username) {Username = username});
        }

        private void UpdateCustomer(string username, string name, string email)
        {
            _customerService.Execute(new UpdateCustomerCommand(username) { Name = name, Email = email});
        }

        private SqlLiteEventSubscriber<IEvent> SqliteSubscriber(DatabaseConfiguration config)
        {
            CreateEventTable(config);
            return new SqlLiteEventSubscriber<IEvent>(config, new JsonEventSerializer());;
        }

        private void CreateEventTable(DatabaseConfiguration configuration)
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