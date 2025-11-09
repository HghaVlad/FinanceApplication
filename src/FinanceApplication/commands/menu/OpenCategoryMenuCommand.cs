using FinanceApplication.commands.category;
using FinanceAccountLibrary.Facades;
using FinanceApplication.Factories;
using FinanceApplication.services;

namespace FinanceApplication.commands.menu
{
    public class OpenCategoryMenuCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;
        private readonly CommandDecoratorFactory _decoratorFactory;
        private readonly IExportService _exportService;

        public OpenCategoryMenuCommand(CategoryProcessingFacade categoryFacade,
            CommandDecoratorFactory decoratorFactory, IExportService exportService)
        {
            _categoryFacade = categoryFacade;
            _decoratorFactory = decoratorFactory;
            _exportService = exportService;
        }

        public string Name => "Open category menu";

        public Task Execute()
        {
            List<ICommand> commands = new List<ICommand>
            {
                new CreateCategoryCommand(_categoryFacade),
                new ShowCategoryCommand(_categoryFacade),
                new UpdateCategoryCommand(_categoryFacade),
                new DeleteCategoryCommand(_categoryFacade),
                new ListCategoriesCommand(_categoryFacade),
                new ExportCategoryDataCommand(_exportService, _categoryFacade)
            }.Select(cmd => _decoratorFactory.Decorate(cmd)).ToList();

            Menu menu = new Menu("Categories", commands);
            menu.Run();
            return Task.CompletedTask;
        }
    }
}