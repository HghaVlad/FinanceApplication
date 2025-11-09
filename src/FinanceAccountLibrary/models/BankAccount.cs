using FinanceAccountLibrary.Export;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountLibrary.models
{
    public class BankAccount : Entity, IExportable
    {
        [StringLength(100, MinimumLength = 1)] public string Name { get; set; }
        public decimal Balance { get; set; }


        public BankAccount(Guid id, string name, decimal balance) : base(id)
        {
            Name = name;
            Balance = balance;
        }


        public override string ToString()
        {
            return $"The BankAccount № {Id} {Name} has a balance of {Balance}";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}