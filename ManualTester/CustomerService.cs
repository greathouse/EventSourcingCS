using System.Collections.Generic;
using System.Linq;
using GreenMoonSoftware.EventSourcing.Core;
using GreenMoonSoftware.EventSourcing.Core.Command;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.SqlLite;

namespace ConsoleApp1
{
    public class CustomerService
    {
        private readonly IBus<IEvent, IEventSubscriber<IEvent>> _bus;
        private readonly SqlLiteAggregateQuery<CustomerAggregate> _query;
        private readonly DatabaseEventSubscriber<IEvent> _store;

        public CustomerService(IBus<IEvent, IEventSubscriber<IEvent>> bus, SqlLiteAggregateQuery<CustomerAggregate> query, DatabaseEventSubscriber<IEvent> store)
        {
            _bus = bus;
            _query = query;
            _store = store;
        }

        public void Execute(ICommand command)
        {
            var aggregate = _query.Retrieve(command.AggregateId);
            var newEvents = AggregateCommandApplier.Apply(aggregate, command);
            Save(newEvents);
            Raise(newEvents);
        }

        private void Save(EventList newEvents)
        {
            newEvents.ForEach(x => _store.OnEvent(x));
        }

        private void Raise(EventList newEvents)
        {
            newEvents.ForEach(x => _bus.Post(x));
        }
    }
}