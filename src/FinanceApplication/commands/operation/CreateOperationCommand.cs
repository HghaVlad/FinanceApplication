using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models.Enums;

namespace FinanceApplication.commands.operation
{
    public class CreateOperationCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;

        public CreateOperationCommand(OperationProcessingFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public string Name => "Create new operation command";

        public async Task Execute()
        {
            Console.WriteLine("New Operation:");
            Console.WriteLine("==============");
            try
            {
                // Get operation type
                Console.WriteLine("Enter operation type (Income/Outcome):");
                string? typeInput = Console.ReadLine();
                if (!Enum.TryParse(typeInput, true, out OperationType type))
                {
                    Console.WriteLine("Invalid operation type. Please enter 'Income' or 'Outcome'.");
                    return;
                }

                // Get bank account ID
                Console.WriteLine("Enter bank account ID (GUID):");
                if (!Guid.TryParse(Console.ReadLine(), out Guid bankAccountId))
                {
                    Console.WriteLine("Invalid bank account ID format.");
                    return;
                }

                // Get amount
                Console.WriteLine("Enter amount:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                {
                    Console.WriteLine("Invalid amount. Please enter a positive number.");
                    return;
                }

                // Get description
                Console.WriteLine("Enter description (optional, press Enter to skip):");
                string? description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = null;
                }

                // Get category ID
                Console.WriteLine("Enter category ID (GUID):");
                if (!Guid.TryParse(Console.ReadLine(), out Guid categoryId))
                {
                    Console.WriteLine("Invalid category ID format.");
                    return;
                }

                Operation operation =
                    await _operationFacade.CreateOperation(type, bankAccountId, amount, description, categoryId);
                Console.WriteLine($"Operation created successfully with ID: {operation.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating operation: {ex.Message}");
            }
        }
    }
}