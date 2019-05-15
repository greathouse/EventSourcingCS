using System.IO;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public interface IEventSerializer
    {
        byte[] Serialize(IEvent e);
        IEvent Deserialize(string eventType, byte[] stream);
    }
}