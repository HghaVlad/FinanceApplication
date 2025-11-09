using FinanceAccountLibrary.Export;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;
using FinanceApplication.services;

namespace FinanceApplication.commands.operation
{
    public class ExportOperationDataCommand(
        IExportService exportService,
        OperationProcessingFacade operationFacade)
        : BaseExportCommand(exportService)
    {
        protected override async Task<List<IExportable>> GetDataAsync()
        {
            Console.WriteLine("Enter bank account ID:");
            if (Guid.TryParse(Console.ReadLine(), out Guid bankAccountId))
            {
                List<Operation> operations = await operationFacade.GetOperationsByBankAccountId(bankAccountId);
                return operations.Cast<IExportable>().ToList();
            }

            Console.WriteLine("Invalid ID format.");
            return new List<IExportable>();
        }

        protected override string GetEntityName()
        {
            return "Operation";
        }
    }
}