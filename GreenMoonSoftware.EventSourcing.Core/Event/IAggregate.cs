namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface IAggregate
    {
        string Id { get; }
        void Apply(EventList events);
    }
}