using FinanceAccountLibrary.models;
using FinanceAccountLibrary.repository;
using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Repositories.sqlite
{
    public class SqliteBankAccountRepository : IBankAccountRepository
    {
        private readonly FinanceDbContext _context;

        public SqliteBankAccountRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> CreateAccount(BankAccount account)
        {
            await _context.BankAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> UpdateAccount(BankAccount account)
        {
            _context.BankAccounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount?> GetAccountById(Guid id)
        {
            return await _context.BankAccounts.FindAsync(id);
        }

        public async Task<bool> DeleteAccount(BankAccount account)
        {
            _context.BankAccounts.Remove(account);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<BankAccount>> GetAllAccounts()
        {
            return await _context.BankAccounts.ToListAsync();
        }
    }
}
