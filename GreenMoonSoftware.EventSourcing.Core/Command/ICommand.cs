using System;

namespace GreenMoonSoftware.EventSourcing.Core.Command
{
    public interface ICommand
    {
        string AggregateId { get; }
    }
}