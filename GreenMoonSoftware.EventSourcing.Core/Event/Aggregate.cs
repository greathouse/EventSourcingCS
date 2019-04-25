namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface Aggregate
    {
        string Id { get; }
        void Apply(EventList events);
    }
}