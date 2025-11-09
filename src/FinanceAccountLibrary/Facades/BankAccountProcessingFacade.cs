using FinanceAccountLibrary.factories;
using FinanceAccountLibrary.models;
using FinanceAccountLibrary.repository;

namespace FinanceAccountLibrary.Facades
{
    public class BankAccountProcessingFacade
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IOperationRepository _operationRepository;

        public BankAccountProcessingFacade(IBankAccountRepository bankAccountRepository,
            IOperationRepository operationRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _operationRepository = operationRepository;
        }


        public async Task<BankAccount> CreateAccount(string name, decimal balance)
        {
            BankAccount account = BankAccountFactory.Create(name, balance);
            return await _bankAccountRepository.CreateAccount(account);
        }
        
        public async Task<BankAccount?> GetAccountById(Guid id)
        {
            return await _bankAccountRepository.GetAccountById(id);
        }

        public async Task<BankAccount> UpdateAccount(Guid id, string name, decimal balance)
        {
            BankAccount? account = await _bankAccountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.Name = name;
            account.Balance = balance;
            return await _bankAccountRepository.UpdateAccount(account);
        }

        public async Task<bool> DeleteAccount(Guid id)
        {
            BankAccount? account = await _bankAccountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if ((await _operationRepository.GetOperationsByBankAccountId(id)).Any())
            {
                throw new Exception("Account has operations and cant be deleted");
            }

            return await _bankAccountRepository.DeleteAccount(account);
        }
        
        public async Task<List<BankAccount>> GetAllAccounts()
        {
            return await _bankAccountRepository.GetAllAccounts();
        }
    }
}