using System;
using System.Text;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Json;
using Newtonsoft.Json;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.JsonTest
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldBeAbleToSerializerAndDeserialize()
        {
            var obj = new TestSimpleObject(Guid.NewGuid().ToString())
            {
                StringType = "TestString",
                TrueBoolean = true,
                FalseBoolean = false,
                Double = 69.69d,
                Integer = 69
            };
            var serializer = new JsonEventSerializer();

            var serial = serializer.Serialize(obj);
            var actual = (TestSimpleObject)serializer.Deserialize(typeof(TestSimpleObject).FullName, serial);
            
            Assert.Equal(obj.StringType, actual.StringType);
            Assert.Equal(obj.TrueBoolean, actual.TrueBoolean);
            Assert.Equal(obj.FalseBoolean, actual.FalseBoolean);
            Assert.Equal(obj.Double, actual.Double);
            Assert.Equal(obj.Integer, actual.Integer);
        }
    }
    
    public class TestSimpleObject : AbstractEvent
    {
        public string StringType { get; set; }
        public bool TrueBoolean { get; set; }
        public bool FalseBoolean { get; set; }
        public double Double { get; set; }
        public int Integer { get; set; }

        public TestSimpleObject(string aggregateId) : base(aggregateId, typeof(TestSimpleObject).FullName)
        {
        }
    }
}