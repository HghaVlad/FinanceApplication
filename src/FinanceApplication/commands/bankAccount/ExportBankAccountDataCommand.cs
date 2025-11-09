using FinanceAccountLibrary.Export;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;
using FinanceApplication.services;

namespace FinanceApplication.commands.bankaccount
{
    public class ExportBankAccountDataCommand(
        IExportService exportService,
        BankAccountProcessingFacade bankAccountFacade)
        : BaseExportCommand(exportService)
    {
        protected override async Task<List<IExportable>> GetDataAsync()
        {
            List<BankAccount> accounts = await bankAccountFacade.GetAllAccounts();
            return accounts.Cast<IExportable>().ToList();
        }

        protected override string GetEntityName()
        {
            return "Bank Account";
        }
    }
}