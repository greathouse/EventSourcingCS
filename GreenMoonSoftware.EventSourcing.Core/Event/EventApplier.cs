using System;
using Microsoft.CSharp.RuntimeBinder;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class EventApplier
    {
        public static void Apply(object obj, IEvent @event)
        {
            try
            {
                dynamic x = obj;
                x.Handle((dynamic) @event);
            }
            catch (RuntimeBinderException e)
            {
                Console.WriteLine($"Object of type '{obj.GetType().FullName}' could not handle event '{@event.GetType().FullName}'");
            }
        }
    }
}