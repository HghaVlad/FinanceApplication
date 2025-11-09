using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.operation
{
    public class DeleteOperationCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;

        public DeleteOperationCommand(OperationProcessingFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public string Name => "Delete operation command";

        public async Task Execute()
        {
            Console.WriteLine("Delete Operation:");
            Console.WriteLine("=================");
            try
            {
                Console.WriteLine("Enter operation ID to delete:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    Operation? operation = await _operationFacade.GetOperationById(id);
                    if (operation != null)
                    {
                        Console.WriteLine($"Operation found: {operation.Type} of {operation.Amount:C}");
                        Console.WriteLine("Are you sure you want to delete it? (y/N)");
                        string? confirmation = Console.ReadLine();

                        if (confirmation?.ToLower() == "y")
                        {
                            bool result = await _operationFacade.DeleteOperation(id);
                            if (result)
                            {
                                Console.WriteLine("Operation deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete operation.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Delete operation cancelled.");
                        }
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
                Console.WriteLine($"Error deleting operation: {ex.Message}");
            }
        }
    }
}
