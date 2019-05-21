using System;
using GreenMoonSoftware.EventSourcing.Core.Event;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Event
{
    public class ObjectEventSerializerTest
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            var expected = new ObjectSerializerTestEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                EventDateTime = DateTime.Now
            };
            
            var serializer = new ObjectEventSerializer();
            var serial = serializer.Serialize(expected);
            
            var actual = serializer.Deserialize("", serial);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal((string) expected.Type, actual.Type);
            Assert.Equal((string) expected.AggregateId, actual.AggregateId);
            Assert.Equal(expected.EventDateTime, actual.EventDateTime);
        }
    }

    [Serializable]
    public class ObjectSerializerTestEvent : IEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set;}
        public string Type { get; set;}
        public DateTime EventDateTime { get; set;}
    }
}