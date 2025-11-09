using FinanceAccountLibrary.models;
using FinanceAccountLibrary.models.Enums;

namespace FinanceAccountLibrary.factories
{
    public static class OperationFactory
    {
        public static Operation Create(OperationType type, Guid bankAccountId, decimal amount, string? description,
            Guid categoryId)
        {
            return new Operation(Guid.NewGuid(), type, bankAccountId, amount, DateTime.Now, description, categoryId);
        }
    }
}