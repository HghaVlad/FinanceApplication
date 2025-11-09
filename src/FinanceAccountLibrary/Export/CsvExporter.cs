using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.Export
{
    public class CsvExporter : ExporterBase
    {
        protected override void WriteHeader(string header)
        {
            Content.AppendLine(header);
        }

        public override void Visit(BankAccount bankAccount)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Id,Name,Balance");
                HeaderWritten = true;
            }
            Content.AppendLine($"{bankAccount.Id},{bankAccount.Name},{bankAccount.Balance}");
        }

        public override void Visit(Category category)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Id,Type,Name");
                HeaderWritten = true;
            }
            Content.AppendLine($"{category.Id},{category.Type},{category.Name}");
        }

        public override void Visit(Operation operation)
        {
            if (!HeaderWritten)
            {
                WriteHeader("Id,Type,BankAccountId,Amount,Date,CategoryId,Description");
                HeaderWritten = true;
            }
            Content.AppendLine($"{operation.Id},{operation.Type},{operation.BankAccountId},{operation.Amount},{operation.Date:yyyy-MM-dd HH:mm:ss},{operation.CategoryId},{operation.Description}");
        }
    }
}