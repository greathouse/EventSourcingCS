using System;
using System.IO;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Database;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.DatabaseTest
{
    public class ObjectEventSerializerTest
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            var expected = new TestEvent
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
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.AggregateId, actual.AggregateId);
            Assert.Equal(expected.EventDateTime, actual.EventDateTime);
        }
    }

    [Serializable]
    public class TestEvent : IEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set;}
        public string Type { get; set;}
        public DateTime EventDateTime { get; set;}
    }
}