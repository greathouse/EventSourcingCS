using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Core
{
    public interface IBus<T, S> where T: IEvent where S: IEventSubscriber<T>
    {
        void Post(T payload);
        IBus<T, S> Register(S subscriber);
        void Unregister(S subscriber);
    }
}