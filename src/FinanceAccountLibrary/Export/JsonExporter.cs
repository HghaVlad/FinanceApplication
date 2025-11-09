using FinanceAccountLibrary.models;
using System.Text.Json;

namespace FinanceAccountLibrary.Export
{
    public class JsonExporter : ExporterBase
    {
        private bool _isArrayStarted;

        protected override void WriteHeader(string header)
        {
            Content.AppendLine("[");
            _isArrayStarted = true;
        }

        public override void Visit(BankAccount bankAccount)
        {
            if (!HeaderWritten)
            {
                WriteHeader("BankAccounts");
                HeaderWritten = true;
            }

            string json = JsonSerializer.Serialize(new
            {
                bankAccount.Id,
                bankAccount.Name,
                bankAccount.Balance
            });

            Content.AppendLine($"  {json},");
        }

        public override void Visit(Category category)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Categories");
                HeaderWritten = true;
            }

            string json = JsonSerializer.Serialize(new
            {
                category.Id,
                category.Type,
                category.Name
            });

            Content.AppendLine($"  {json},");
        }

        public override void Visit(Operation operation)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Operations");
                HeaderWritten = true;
            }

            string json = JsonSerializer.Serialize(new
            {
                operation.Id,
                operation.Type,
                operation.BankAccountId,
                operation.Amount,
                Date = operation.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                operation.CategoryId,
                operation.Description
            });

            Content.AppendLine($"  {json},");
        }

        public override string GetContent()
        {
            if (Content.Length > 0 && Content.ToString().EndsWith(",\n"))
            {
                Content.Remove(Content.Length - 2, 1); // Remove trailing comma
            }

            if (_isArrayStarted)
            {
                Content.AppendLine("]");
            }

            return Content.ToString();
        }
    }
}
