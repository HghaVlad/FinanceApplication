using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.repository
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> CreateAccount(BankAccount account);
        Task<BankAccount> UpdateAccount(BankAccount account);
        Task<BankAccount?> GetAccountById(Guid id);
        Task<bool> DeleteAccount(BankAccount account);
        Task<List<BankAccount>> GetAllAccounts();
    }
}