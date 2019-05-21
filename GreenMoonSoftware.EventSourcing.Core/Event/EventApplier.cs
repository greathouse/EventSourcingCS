using System;
using Microsoft.CSharp.RuntimeBinder;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class EventApplier
    {
        public static void Apply(IAggregate aggregate, IEvent @event)
        {
            try
            {
                dynamic x = aggregate;
                x.Handle((dynamic) @event);
            }
            catch (RuntimeBinderException e)
            {
                throw new UnhandledEventException($"Aggregate of type '{aggregate.GetType().FullName}' could not handle event '{@event.GetType().FullName}'", e);
            }
        }
    }
}