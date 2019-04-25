using System;
using System.Collections.Generic;
using GreenMoonSoftware.EventSourcing.Core.Command;
using GreenMoonSoftware.EventSourcing.Core.Event;
using GreenMoonSoftware.EventSourcing.CoreTest.Event;
using Xunit;

namespace GreenMoonSoftware.EventSourcing.CoreTest.Command
{
    public class AggregateCommandApplierTest
    {
        [Fact]
        public void GivenAggregateWithHandleMethod_ShouldCallDefaultHandleMethod()
        {
            var aggregate = new TestAggregate();
            
            AggregateCommandApplier.Apply(aggregate, new TestCommand1());
            AggregateCommandApplier.Apply(aggregate, new TestCommand2());
            
            Assert.True(aggregate.Command1Handled);
            Assert.True(aggregate.Command2Handled);
        }
    }

    public class TestAggregate : IAggregate
    {
        public bool Command1Handled { get; private set; } = false;

        public bool Command2Handled { get; private set; } = false;

        public string Id { get; }

        public IEnumerable<IEvent> Handle(TestCommand1 command1)
        {
            Command1Handled = true;
            return new List<IEvent>();
        }

        public IEnumerable<IEvent> Handle(TestCommand2 command)
        {
            Command2Handled = true;
            return new List<IEvent>();
        }
        
        public void Apply(EventList events)
        {
            
        }
    }

    public class TestCommand1 : ICommand
    {
        public string AggregateId { get; }
    }

    public class TestCommand2 : ICommand
    {
        public string AggregateId { get; }
    }
}