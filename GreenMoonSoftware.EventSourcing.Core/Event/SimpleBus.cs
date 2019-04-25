using System.Collections.Generic;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class SimpleBus : IBus<IEvent, IEventSubscriber<IEvent>>
    {
        private List<IEventSubscriber<IEvent>> _subscribers = new List<IEventSubscriber<IEvent>>();
            
        public void Post(IEvent payload)
        {
            _subscribers.ForEach(x => x.OnEvent(payload));
        }

        public IBus<IEvent, IEventSubscriber<IEvent>> Register(IEventSubscriber<IEvent> subscriber)
        {
            _subscribers.Add(subscriber);
            return this;
        }

        public void Unregister(IEventSubscriber<IEvent> subscriber)
        {
            throw new System.NotImplementedException();
        }
    }
}