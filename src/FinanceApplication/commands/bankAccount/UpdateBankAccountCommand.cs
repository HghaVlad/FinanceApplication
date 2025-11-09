using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.bankaccount
{
    public class UpdateBankAccountCommand : ICommand
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;

        public UpdateBankAccountCommand(BankAccountProcessingFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public string Name => "Update bank account";

        public async Task Execute()
        {
            Console.WriteLine("Update Bank Account:");
            Console.WriteLine("====================");
            try
            {
                Console.WriteLine("Enter bank account ID to update:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    BankAccount? account = await _bankAccountFacade.GetAccountById(id);
                    if (account != null)
                    {
                        Console.WriteLine($"Current account: {account.Name} (Balance: {account.Balance:C})");

                        // Update name
                        Console.WriteLine($"Enter new name (current: {account.Name}) or press Enter to keep current:");
                        string? nameInput = Console.ReadLine();
                        string newName = string.IsNullOrWhiteSpace(nameInput) ? account.Name : nameInput;

                        // Update balance
                        Console.WriteLine($"Enter new balance (current: {account.Balance:C}) or press Enter to keep current:");
                        string? balanceInput = Console.ReadLine();
                        decimal newBalance = account.Balance;
                        if (!string.IsNullOrWhiteSpace(balanceInput) && decimal.TryParse(balanceInput, out decimal parsedBalance) && parsedBalance >= 0)
                        {
                            newBalance = parsedBalance;
                        }

                        BankAccount updatedAccount = await _bankAccountFacade.UpdateAccount(id, newName, newBalance);
                        Console.WriteLine($"Bank account updated successfully!");
                        Console.WriteLine($"ID: {updatedAccount.Id}, Name: {updatedAccount.Name}, Balance: {updatedAccount.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Bank account not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating bank account: {ex.Message}");
            }
        }
    }
}
