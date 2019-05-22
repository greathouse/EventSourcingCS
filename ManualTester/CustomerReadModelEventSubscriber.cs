using System.Data.SQLite;
using ConsoleApp1.Events;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;

namespace ConsoleApp1
{
    public class CustomerReadModelEventSubscriber : IEventSubscriber<IEvent>
    {
        private readonly DatabaseConfiguration _configuration;

        public CustomerReadModelEventSubscriber(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnEvent(IEvent @event)
        {
            EventApplier.Apply(this, @event);
        }

        public void Handle(CreateCustomerEvent e)
        {
            using (var conn = _configuration.CreateConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "insert into Customer (id, username) values (@username, @username)";
                cmd.AddWithValue("@username", e.Username);
                cmd.ExecuteNonQuery();
            }
        }

        public void Handle(UpdateCustomerEvent e)
        {
            using (var conn = _configuration.CreateConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "update Customer set name = @name, email = @email where username = @username";
                cmd.AddWithValue("@name", e.Name);
                cmd.AddWithValue("@email", e.Email);
                cmd.AddWithValue("@username", e.AggregateId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}