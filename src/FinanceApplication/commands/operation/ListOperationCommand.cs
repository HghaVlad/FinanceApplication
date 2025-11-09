using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.operation
{
    public class ListOperationsCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;

        public ListOperationsCommand(OperationProcessingFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public string Name => "List operations by bank account command";

        public async Task Execute()
        {
            Console.WriteLine("List Operations:");
            Console.WriteLine("================");
            try
            {
                Console.WriteLine("Enter bank account ID:");
                if (Guid.TryParse(Console.ReadLine(), out Guid bankAccountId))
                {
                    List<Operation> operations = await _operationFacade.GetOperationsByBankAccountId(bankAccountId);

                    if (operations.Any())
                    {
                        Console.WriteLine($"Operations for bank account {bankAccountId}:");
                        Console.WriteLine("----------------------------------------");
                        
                        foreach (Operation operation in operations)
                        {
                            Console.WriteLine($"ID: {operation.Id}, Type: {operation.Type}, Amount: {operation.Amount:C}, " +
                                            $"Description: {operation.Description ?? "No description"}, " +
                                            $"Category: {operation.CategoryId}, Date: {operation.Date}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No operations found for this bank account.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid bank account ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing operations: {ex.Message}");
            }
        }
    }
}
