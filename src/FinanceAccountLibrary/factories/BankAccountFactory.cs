using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.factories
{
    public static class BankAccountFactory
    {
        public static BankAccount Create(string name, decimal balance)
        {
            return new BankAccount(Guid.NewGuid(), name, balance);
        }
    }
}