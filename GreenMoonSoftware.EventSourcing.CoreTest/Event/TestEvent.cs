using System;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class TestEvent : AbstractEvent
    {
        public TestEvent(string id) : base(id, Guid.NewGuid().ToString())
        {
            
        }
    }
}