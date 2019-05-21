using System;
using System.Linq;
using System.Text;
using GreenMoonSoftware.EventSourcing.Core.Event;
using Newtonsoft.Json;

namespace GreenMoonSoftware.EventSourcing.Json
{
    public class JsonEventSerializer : IEventSerializer
    {
        public byte[] Serialize(IEvent e)
        {
            var json = JsonConvert.SerializeObject(e);
            return Encoding.UTF8.GetBytes(json);
        }

        public IEvent Deserialize(string eventType, byte[] stream)
        {
            return (IEvent) JsonConvert.DeserializeObject(ToJson(stream), FindType(eventType));
        }

        private static string ToJson(byte[] stream)
        {
            return Encoding.UTF8.GetString(stream, 0, stream.Length);
        }

        private static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }
    }
}