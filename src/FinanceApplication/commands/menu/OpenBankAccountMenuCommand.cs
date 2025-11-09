using FinanceApplication.commands.bankaccount;
using FinanceAccountLibrary.Facades;
using FinanceApplication.Factories;
using FinanceApplication.services;

namespace FinanceApplication.commands.menu
{
    public class OpenBankAccountMenuCommand : ICommand
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;
        private readonly CommandDecoratorFactory _decoratorFactory;
        private readonly IExportService _exportService;

        public OpenBankAccountMenuCommand(BankAccountProcessingFacade bankAccountFacade,
            CommandDecoratorFactory decoratorFactory, IExportService exportService)
        {
            _bankAccountFacade = bankAccountFacade;
            _decoratorFactory = decoratorFactory;
            _exportService = exportService;
        }

        public string Name => "Open bank account menu";

        public Task Execute()
        {
            List<ICommand> commands = new List<ICommand>
            {
                new CreateBankAccountCommand(_bankAccountFacade),
                new ShowBankAccountCommand(_bankAccountFacade),
                new UpdateBankAccountCommand(_bankAccountFacade),
                new DeleteBankAccountCommand(_bankAccountFacade),
                new ListBankAccountsCommand(_bankAccountFacade),
                new ExportBankAccountDataCommand(_exportService, _bankAccountFacade)
            }.Select(cmd => _decoratorFactory.Decorate(cmd)).ToList();

            Menu menu = new Menu("Bank Accounts", commands);
            menu.Run();
            return Task.CompletedTask;
        }
    }
}