using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Core.Command
{
    public class AggregateCommandApplier
    {
        public static IEnumerable<IEvent> Apply(IAggregate aggregate, ICommand command)
        {
            dynamic x = aggregate;
            return x.Handle((dynamic)command);
        }
    }
}