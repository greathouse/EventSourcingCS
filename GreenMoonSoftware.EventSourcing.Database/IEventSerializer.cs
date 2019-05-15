using System.IO;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public interface IEventSerializer
    {
        Stream Serialize(IEvent e);
        IEvent Deserialize(string eventType, Stream stream);
    }
}