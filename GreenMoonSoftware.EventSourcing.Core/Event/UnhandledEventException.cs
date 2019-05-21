using System;

namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public class UnhandledEventException : Exception
    {
        public UnhandledEventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}