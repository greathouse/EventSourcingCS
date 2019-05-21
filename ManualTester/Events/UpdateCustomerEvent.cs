using GreenMoonSoftware.EventSourcing.Core.Event;

namespace ConsoleApp1.Events
{
    public class UpdateCustomerEvent : AbstractEvent
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        
        public UpdateCustomerEvent(string aggregateId, string name, string email) : base(aggregateId, typeof(UpdateCustomerEvent).FullName)
        {
            Name = name;
            Email = email;
        }
    }
}