using FinanceAccountLibrary.factories;
using FinanceAccountLibrary.models;
using FinanceAccountLibrary.models.Enums;
using FinanceAccountLibrary.repository;

namespace FinanceAccountLibrary.Facades
{
    public class OperationProcessingFacade
    {
        private readonly IOperationRepository _operationRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public OperationProcessingFacade(IOperationRepository operationRepository,
            ICategoryRepository categoryRepository, IBankAccountRepository bankAccountRepository)
        {
            _operationRepository = operationRepository;
            _categoryRepository = categoryRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Operation> CreateOperation(OperationType type, Guid bankAccountId, decimal amount,
            string? description, Guid categoryId)
        {
            Category? category = await _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            BankAccount? bankAccount = await _bankAccountRepository.GetAccountById(bankAccountId);
            if (bankAccount == null)
            {
                throw new Exception("Bank account not found");
            }

            Operation operation = OperationFactory.Create(type, bankAccountId, amount, description, categoryId);
            return await _operationRepository.CreateOperation(operation);
        }

        public Task<Operation?> GetOperationById(Guid id)
        {
            return _operationRepository.GetOperationById(id);
        }

        public Task<List<Operation>> GetOperationsByBankAccountId(Guid id)
        {
            return _operationRepository.GetOperationsByBankAccountId(id);
        }

        public Task<List<Operation>> GetOperationsByCategoryId(Guid id)
        {
            return _operationRepository.GetOperationsByCategoryId(id);
        }

        public async Task<Operation> UpdateOperation(Guid id, OperationType type, Guid bankAccountId, decimal amount,
            string? description, Guid categoryId)
        {
            Operation? operation = await _operationRepository.GetOperationById(id);
            if (operation == null)
            {
                throw new Exception("Operation not found");
            }

            Category? category = await _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            BankAccount? bankAccount = await _bankAccountRepository.GetAccountById(bankAccountId);
            if (bankAccount == null)
            {
                throw new Exception("Bank account not found");
            }

            operation.Type = type;
            operation.BankAccountId = bankAccountId;
            operation.Amount = amount;
            operation.Description = description;
            operation.CategoryId = categoryId;


            return await _operationRepository.UpdateOperation(operation);
        }

        public async Task<bool> DeleteOperation(Guid id)
        {
            Operation? operation = await _operationRepository.GetOperationById(id);
            if (operation == null)
            {
                throw new Exception("Operation not found");
            }

            return await _operationRepository.DeleteOperation(operation);
        }
        
    }
}