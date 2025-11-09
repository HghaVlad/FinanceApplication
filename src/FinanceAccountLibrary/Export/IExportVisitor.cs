using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.Export
{
    public interface IExportVisitor
    {
        void Visit(BankAccount bankAccount);
        void Visit(Category category);
        void Visit(Operation operation);
    }
}