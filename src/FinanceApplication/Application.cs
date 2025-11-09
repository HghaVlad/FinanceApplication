using FinanceApplication.commands;
using FinanceApplication.commands.menu;
using FinanceAccountLibrary.Facades;
using FinanceApplication.Factories;
using FinanceApplication.services;

namespace FinanceApplication
{
    public class Application
    {
        private readonly BankAccountProcessingFacade _bankAccountFacade;
        private readonly CategoryProcessingFacade _categoryFacade;
        private readonly OperationProcessingFacade _operationFacade;
        private readonly CommandDecoratorFactory _decoratorFactory;
        private readonly IExportService _exportService;

        public Application(
            BankAccountProcessingFacade bankAccountFacade,
            CategoryProcessingFacade categoryFacade,
            OperationProcessingFacade operationFacade,
            CommandDecoratorFactory decoratorFactory, IExportService exportService)
        {
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
            _decoratorFactory = decoratorFactory;
            _exportService = exportService;
        }

        public void Run()
        {
            List<ICommand> commands = new List<ICommand>
            {
                new OpenBankAccountMenuCommand(_bankAccountFacade, _decoratorFactory, _exportService),
                new OpenCategoryMenuCommand(_categoryFacade, _decoratorFactory, _exportService),
                new OpenOperationMenuCommand(_operationFacade, _decoratorFactory, _exportService)
            }.Select(cmd => _decoratorFactory.Decorate(cmd)).ToList();

            Menu mainMenu = new Menu("Finance Application", commands);
            mainMenu.Run();
            Console.WriteLine("GoodBye!");
        }
    }
}