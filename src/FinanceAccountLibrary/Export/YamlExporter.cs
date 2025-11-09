using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.Export
{
    public class YamlExporter : ExporterBase
    {
        protected override void WriteHeader(string header)
        {
            Content.AppendLine("---");
        }

        public override void Visit(BankAccount bankAccount)
        {
            if (!HeaderWritten)
            {
                WriteHeader("BankAccounts");
                HeaderWritten = true;
            }

            Content.AppendLine($"- id: {bankAccount.Id}");
            Content.AppendLine($"  name: {bankAccount.Name}");
            Content.AppendLine($"  balance: {bankAccount.Balance}");
        }

        public override void Visit(Category category)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Categories");
                HeaderWritten = true;
            }

            Content.AppendLine($"- id: {category.Id}");
            Content.AppendLine($"  type: {category.Type}");
            Content.AppendLine($"  name: {category.Name}");
        }

        public override void Visit(Operation operation)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Operations");
                HeaderWritten = true;
            }

            Content.AppendLine($"- id: {operation.Id}");
            Content.AppendLine($"  type: {operation.Type}");
            Content.AppendLine($"  bankAccountId: {operation.BankAccountId}");
            Content.AppendLine($"  amount: {operation.Amount}");
            Content.AppendLine($"  date: {operation.Date:yyyy-MM-dd HH:mm:ss}");
            Content.AppendLine($"  categoryId: {operation.CategoryId}");
            Content.AppendLine($"  description: {operation.Description ?? "null"}");
        }
    }
}