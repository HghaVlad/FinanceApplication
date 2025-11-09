using FinanceAccountLibrary.Export;
using FinanceAccountLibrary.models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountLibrary.models
{
    public class Category : Entity, IExportable
    {
        public CategoryType Type { get; set; }
        [StringLength(100, MinimumLength = 1)] public string Name { get; set; }

        public Category(Guid id, CategoryType type, string name) : base(id)
        {
            Type = type;
            Name = name;
        }

        public override string ToString()
        {
            return $"The Category № {Id} {Name} is {Type}";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}