using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;
using FinanceAccountLibrary.models.Enums;

namespace FinanceApplication.commands.operation
{
    public class UpdateOperationCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;

        public UpdateOperationCommand(OperationProcessingFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public string Name => "Update operation command";

        public async Task Execute()
        {
            Console.WriteLine("Update Operation:");
            Console.WriteLine("=================");
            try
            {
                Console.WriteLine("Enter operation ID to update:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    Operation? operation = await _operationFacade.GetOperationById(id);
                    if (operation != null)
                    {
                        Console.WriteLine($"Current operation: {operation.Type} of {operation.Amount:C}");
                        Console.WriteLine($"Bank Account ID: {operation.BankAccountId}");
                        Console.WriteLine($"Description: {operation.Description ?? "No description"}");
                        Console.WriteLine($"Category ID: {operation.CategoryId}");

                        // Update operation type
                        Console.WriteLine(
                            $"Enter new type (current: {operation.Type}) or press Enter to keep current:");
                        string? typeInput = Console.ReadLine();
                        OperationType newType = operation.Type;
                        if (!string.IsNullOrWhiteSpace(typeInput) &&
                            Enum.TryParse(typeInput, true, out OperationType parsedType))
                        {
                            newType = parsedType;
                        }

                        // Update bank account ID
                        Console.WriteLine(
                            $"Enter new bank account ID (current: {operation.BankAccountId}) or press Enter to keep current:");
                        string? bankAccountIdInput = Console.ReadLine();
                        Guid newBankAccountId = operation.BankAccountId;
                        if (!string.IsNullOrWhiteSpace(bankAccountIdInput) &&
                            Guid.TryParse(bankAccountIdInput, out Guid parsedBankAccountId))
                        {
                            newBankAccountId = parsedBankAccountId;
                        }

                        // Update amount
                        Console.WriteLine(
                            $"Enter new amount (current: {operation.Amount:C}) or press Enter to keep current:");
                        string? amountInput = Console.ReadLine();
                        decimal newAmount = operation.Amount;
                        if (!string.IsNullOrWhiteSpace(amountInput) &&
                            decimal.TryParse(amountInput, out decimal parsedAmount) && parsedAmount > 0)
                        {
                            newAmount = parsedAmount;
                        }

                        // Update description
                        Console.WriteLine(
                            $"Enter new description (current: {operation.Description ?? "None"}) or press Enter to keep current:");
                        string? descriptionInput = Console.ReadLine();
                        string? newDescription = operation.Description;
                        if (descriptionInput != null)
                        {
                            newDescription = string.IsNullOrWhiteSpace(descriptionInput) ? null : descriptionInput;
                        }

                        // Update category ID
                        Console.WriteLine(
                            $"Enter new category ID (current: {operation.CategoryId}) or press Enter to keep current:");
                        string? categoryIdInput = Console.ReadLine();
                        Guid newCategoryId = operation.CategoryId;
                        if (!string.IsNullOrWhiteSpace(categoryIdInput) &&
                            Guid.TryParse(categoryIdInput, out Guid parsedCategoryId))
                        {
                            newCategoryId = parsedCategoryId;
                        }

                        Operation updatedOperation = await _operationFacade.UpdateOperation(
                            id, newType, newBankAccountId, newAmount, newDescription, newCategoryId);

                        Console.WriteLine($"Operation updated successfully!");
                        Console.WriteLine(
                            $"ID: {updatedOperation.Id}, Type: {updatedOperation.Type}, Amount: {updatedOperation.Amount:C}");
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
                Console.WriteLine($"Error updating operation: {ex.Message}");
            }
        }
    }
}