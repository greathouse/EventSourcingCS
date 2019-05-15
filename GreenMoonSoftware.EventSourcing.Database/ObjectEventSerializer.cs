using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GreenMoonSoftware.EventSourcing.Core.Event;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public class ObjectEventSerializer : IEventSerializer
    {
        public byte[] Serialize(IEvent e)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, e);
            return ms.ToArray();
        }

        public IEvent Deserialize(string eventType, byte[] stream)
        {
            var bf = new BinaryFormatter();
            return (IEvent) bf.Deserialize(new MemoryStream(stream));
        }
    }
}