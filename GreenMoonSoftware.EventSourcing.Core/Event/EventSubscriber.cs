namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface EventSubscriber<T> where T : IEvent
    {
        void OnEvent(T @event);
    }
}