using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;


namespace FinanceApplication.commands.bankaccount
{
    public class ListBankAccountsCommand : ICommand
    {
        private readonly BankAccountProcessingFacade  _bankAccountFacade;

        public ListBankAccountsCommand(BankAccountProcessingFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public string Name => "List all bank accounts";

        public async Task Execute()
        {
            Console.WriteLine("All Bank Accounts:");
            Console.WriteLine("==================");
            try
            {
                List<BankAccount> accounts = await _bankAccountFacade.GetAllAccounts();

                if (accounts.Any())
                {
                    foreach (BankAccount account in accounts)
                    {
                        Console.WriteLine($"ID: {account.Id}, Name: {account.Name}, Balance: {account.Balance:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No bank accounts found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing bank accounts: {ex.Message}");
            }
        }
    }
}
