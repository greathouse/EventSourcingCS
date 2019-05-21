using System;
using System.Reflection.Metadata;
using ConsoleApp1.Commands;
using ConsoleApp1.Events;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace ConsoleApp1
{
    public class CustomerAggregate : IAggregate
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        
        public void Apply(EventList events)
        {
            events.ForEach(x => EventApplier.Apply(this, x));
        }

        public EventList Handle(CreateCustomerCommand cmd)
        {
            return new EventList(new CreateCustomerEvent(cmd.Username, cmd.Username));
        }

        public EventList Handle(UpdateCustomerCommand cmd)
        {
            return new EventList(new UpdateCustomerEvent(cmd.AggregateId, cmd.Name, cmd.Email));
        }

        public void Handle(CreateCustomerEvent e)
        {
            this.Id = e.AggregateId;
            this.Username = e.Username;
        }
    }
}