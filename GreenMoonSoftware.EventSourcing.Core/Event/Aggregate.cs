namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface Aggregate
    {
        string getId();
        void apply(EventList events);
    }
}