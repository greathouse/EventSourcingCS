using GreenMoonSoftware.EventSourcing.Core.Event;

namespace ConsoleApp1.Events
{
    public class CreateCustomerEvent : AbstractEvent
    {
        public string Username { get; }

        public CreateCustomerEvent(string aggregateId, string username) : base(aggregateId, typeof(CreateCustomerEvent).FullName)
        {
            this.Username = username;
        }
    }
}