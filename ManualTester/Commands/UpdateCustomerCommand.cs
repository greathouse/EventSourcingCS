using GreenMoonSoftware.EventSourcing.Core.Command;

namespace ConsoleApp1.Commands
{
    public class UpdateCustomerCommand : ICommand
    {
        public string AggregateId { get; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UpdateCustomerCommand(string aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}