namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface IEventSubscriber<in T> where T : IEvent
    {
        void OnEvent(T @event);
    }
}