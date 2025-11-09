using FinanceAccountLibrary.models;
using FinanceAccountLibrary.repository;
using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Repositories.sqlite
{
    public class SqliteOperationRepository : IOperationRepository
    {
        private readonly FinanceDbContext _context;

        public SqliteOperationRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> CreateOperation(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task<Operation> UpdateOperation(Operation operation)
        {
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task<Operation?> GetOperationById(Guid id)
        {
            return await _context.Operations.FindAsync(id);
        }

        public async Task<bool> DeleteOperation(Operation operation)
        {
            _context.Operations.Remove(operation);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Operation>> GetAllOperations()
        {
            return await _context.Operations.ToListAsync();
        }

        public async Task<List<Operation>> GetOperationsByBankAccountId(Guid id)
        {
            return await _context.Operations
                .Where(o => o.BankAccountId == id)
                .ToListAsync();
        }

        public async Task<List<Operation>> GetOperationsByCategoryId(Guid id)
        {
            return await _context.Operations
                .Where(o => o.CategoryId == id)
                .ToListAsync();
        }
    }
}
