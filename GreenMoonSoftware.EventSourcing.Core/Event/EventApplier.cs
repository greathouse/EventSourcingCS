namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class EventApplier
    {
        public static void Apply(IAggregate aggregate, IEvent @event)
        {
            dynamic x = aggregate;
            x.Handle((dynamic) @event);
        }
    }
}