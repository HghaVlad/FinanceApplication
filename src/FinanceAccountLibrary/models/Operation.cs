using FinanceAccountLibrary.Export;
using FinanceAccountLibrary.models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountLibrary.models
{
    public class Operation : Entity, IExportable
    {
        public OperationType Type { get; set; }
        public Guid BankAccountId { get; set; }
        private decimal _amount;

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Amount can't be negative");
                }

                _amount = value;
            }
        }

        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(100)] public string? Description { get; set; }


        public Operation(Guid id, OperationType type, Guid bankAccountId, decimal amount,
            DateTime date, string? description, Guid categoryId) : base(id)
        {
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}