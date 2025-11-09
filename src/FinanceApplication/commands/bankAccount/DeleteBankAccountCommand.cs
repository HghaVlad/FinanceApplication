using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;

namespace FinanceApplication.commands.bankaccount
{
    public class DeleteBankAccountCommand : ICommand
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;

        public DeleteBankAccountCommand(BankAccountProcessingFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public string Name => "Delete bank account";

        public async Task Execute()
        {
            Console.WriteLine("Delete Bank Account:");
            Console.WriteLine("====================");
            try
            {
                Console.WriteLine("Enter bank account ID to delete:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    BankAccount? account = await _bankAccountFacade.GetAccountById(id);
                    if (account != null)
                    {
                        Console.WriteLine($"Bank account found: {account.Name} (Balance: {account.Balance:C})");
                        Console.WriteLine("Are you sure you want to delete it? (Y/N)");
                        string? confirmation = Console.ReadLine();

                        if (confirmation?.ToLower() == "y")
                        {
                            bool result = await _bankAccountFacade.DeleteAccount(id);
                            if (result)
                            {
                                Console.WriteLine("Bank account deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete bank account.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Delete operation cancelled.");
                        }
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
                Console.WriteLine($"Error deleting bank account: {ex.Message}");
            }
        }
    }
}
