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

                if (inputs[0] == "replay")
                    program.Replay();
            }
            Console.WriteLine("Done!");
        }

        private readonly CustomerService _customerService;
        private readonly DatabaseConfiguration _configuration;
        private readonly JsonEventSerializer _eventSerializer;
        private readonly SimpleBus _bus;

        public Program()
        {
            _bus = new SimpleBus();
            var connectionString = $"Data Source=/var/folders/7z/9yx_mn5n3jv726jj7r0qyzp40000gn/T/tmp76YuZR.tmp;Version=3;";
            _configuration = new DatabaseConfiguration
            {
                ConnectionString = connectionString,
                TableName = "CustomerEvent"
            };
            _eventSerializer = new JsonEventSerializer();
            var query = new CustomerQuery(_configuration, _eventSerializer);
            _customerService = new CustomerService(_bus, query, SqliteSubscriber(_configuration));
            _bus.Register(new CustomerReadModelEventSubscriber(_configuration));
        }

        private void Replay()
        {
            var events = new EventList();
            using (var conn = new SQLiteConnection(_configuration.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "delete from Customer";
                cmd.ExecuteNonQuery();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from CustomerEvent order by eventDateTime asc";
                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    events.Add(_eventSerializer.Deserialize((string)reader["eventType"], (byte[])reader["Data"]));
                }
            }
            events.ForEach(x => _bus.Post(x));
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
            return new SqlLiteEventSubscriber<IEvent>(config, _eventSerializer);;
        }

        private void CreateEventTable(DatabaseConfiguration configuration)
        {
            using (var conn = new SQLiteConnection(configuration.ConnectionString))
            {
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
}