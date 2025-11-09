using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.repository
{
    public interface IOperationRepository
    {
        Task<Operation> CreateOperation(Operation operation);
        Task<Operation> UpdateOperation(Operation operation);
        Task<Operation?> GetOperationById(Guid id);
        Task<bool> DeleteOperation(Operation operation);
        Task<List<Operation>> GetAllOperations();
        Task<List<Operation>> GetOperationsByBankAccountId(Guid id);
        Task<List<Operation>> GetOperationsByCategoryId(Guid id);
    }
}