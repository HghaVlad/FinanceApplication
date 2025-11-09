using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.operation
{
    public class ShowOperationCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;

        public ShowOperationCommand(OperationProcessingFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public string Name => "Show operation by ID command";

        public async Task Execute()
        {
            Console.WriteLine("Show Operation:");
            Console.WriteLine("===============");
            try
            {
                Console.WriteLine("Enter operation ID:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    Operation? operation = await _operationFacade.GetOperationById(id);
                    if (operation != null)
                    {
                        Console.WriteLine($"ID: {operation.Id}");
                        Console.WriteLine($"Type: {operation.Type}");
                        Console.WriteLine($"Bank Account ID: {operation.BankAccountId}");
                        Console.WriteLine($"Amount: {operation.Amount:C}");
                        Console.WriteLine($"Description: {operation.Description ?? "No description"}");
                        Console.WriteLine($"Category ID: {operation.CategoryId}");
                        Console.WriteLine($"Created: {operation.Date}");
                    }
                    else
                    {
                        Console.WriteLine("Operation not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading operation: {ex.Message}");
            }
        }
    }
}