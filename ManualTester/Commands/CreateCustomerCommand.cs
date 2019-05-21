using GreenMoonSoftware.EventSourcing.Core.Command;

namespace ConsoleApp1.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string AggregateId { get; }

        public string Username { get; set; }

        public CreateCustomerCommand(string aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}