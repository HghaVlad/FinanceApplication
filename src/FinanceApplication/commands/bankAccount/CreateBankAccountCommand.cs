using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.bankaccount
{
    public class CreateBankAccountCommand : ICommand
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;

        public CreateBankAccountCommand(BankAccountProcessingFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public string Name => "Create new bank account";

        public async Task Execute()
        {
            Console.WriteLine("New Bank Account:");
            Console.WriteLine("=================");
            try
            {
                Console.Write("Enter account name: ");
                string? name = Console.ReadLine();
                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("The name can't be null or empty please write it");
                    Console.Write("Enter account name: ");
                    name = Console.ReadLine();
                }

                Console.Write("Enter initial balance: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal balance) && balance >= 0)
                {
                    BankAccount account = await _bankAccountFacade.CreateAccount(name, balance);
                    Console.WriteLine($"Bank account created successfully with ID: {account.Id}");
                }
                else
                {
                    Console.WriteLine("Invalid balance. Please enter a valid positive number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bank account: {ex.Message}");
            }
        }
    }
}
