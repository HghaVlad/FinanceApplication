using FinanceApplication.commands.operation;
using FinanceAccountLibrary.Facades;
using FinanceApplication.Factories;
using FinanceApplication.services;

namespace FinanceApplication.commands.menu
{
    public class OpenOperationMenuCommand : ICommand
    {
        private readonly OperationProcessingFacade _operationFacade;
        private readonly CommandDecoratorFactory _decoratorFactory;
        private readonly IExportService _exportService;

        public OpenOperationMenuCommand(OperationProcessingFacade operationFacade,
            CommandDecoratorFactory decoratorFactory, IExportService exportService)
        {
            _operationFacade = operationFacade;
            _decoratorFactory = decoratorFactory;
            _exportService = exportService;
        }

        public string Name => "Open operations menu";

        public Task Execute()
        {
            List<ICommand> commands = new List<ICommand>
            {
                new CreateOperationCommand(_operationFacade),
                new ShowOperationCommand(_operationFacade),
                new UpdateOperationCommand(_operationFacade),
                new DeleteOperationCommand(_operationFacade),
                new ListOperationsCommand(_operationFacade),
                new ExportOperationDataCommand(_exportService, _operationFacade)
            }.Select(cmd => _decoratorFactory.Decorate(cmd)).ToList();

            Menu menu = new Menu("Operations", commands);
            menu.Run();
            return Task.CompletedTask;
        }
    }
}