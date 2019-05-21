using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Newtonsoft.Json;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.JsonTest
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldBeAbleToSerializerAndDeserialize()
        {
            var obj = ValidSimpleObject();
            
            var serializer = new JsonEventSerializer();
            var serial = serializer.Serialize(obj);
            var actual = (TestSimpleObject)serializer.Deserialize(obj.Type, serial);
            
            Assert.Equal(obj.StringType, actual.StringType);
            Assert.Equal(obj.TrueBoolean, actual.TrueBoolean);
            Assert.Equal(obj.FalseBoolean, actual.FalseBoolean);
            Assert.Equal(obj.Double, actual.Double);
            Assert.Equal(obj.Integer, actual.Integer);
        }

        [Fact]
        public void ShouldSerializeLists()
        {
            var obj = new TestEventWithList(Guid.NewGuid().ToString())
            {
                ListOfObjects = new []
                {
                    ValidSimpleObject(), ValidSimpleObject()
                }
            };
            
            var serializer = new JsonEventSerializer();
            var serial = serializer.Serialize(obj);
            var actual = (TestEventWithList)serializer.Deserialize(obj.Type, serial);
            
            Assert.Equal(obj.ListOfObjects.Count(), actual.ListOfObjects.Count());
        }

        private static TestSimpleObject ValidSimpleObject()
        {
            return new TestSimpleObject(Guid.NewGuid().ToString())
            {
                StringType = "TestString",
                TrueBoolean = true,
                FalseBoolean = false,
                Double = 69.69d,
                Integer = 69
            };
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

    public class TestEventWithList : AbstractEvent
    {
        public IEnumerable<TestSimpleObject> ListOfObjects { get; set; }
        
        public TestEventWithList(string aggregateId) : base(aggregateId, typeof(TestEventWithList).FullName)
        {
        }
    }
}