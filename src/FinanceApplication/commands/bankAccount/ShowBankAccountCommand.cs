using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.bankaccount
{
    public class ShowBankAccountCommand : ICommand
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;

        public ShowBankAccountCommand(BankAccountProcessingFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public string Name => "Show bank account by ID";

        public async Task Execute()
        {
            Console.WriteLine("Show Bank Account:");
            Console.WriteLine("==================");
            try
            {
                Console.WriteLine("Enter bank account ID:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    BankAccount? account = await _bankAccountFacade.GetAccountById(id);
                    if (account != null)
                    {
                        Console.WriteLine($"ID: {account.Id}, Name: {account.Name}, Balance: {account.Balance:C}");
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
                Console.WriteLine($"Error reading bank account: {ex.Message}");
            }
        }
    }
}
