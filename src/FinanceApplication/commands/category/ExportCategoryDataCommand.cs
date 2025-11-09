using FinanceAccountLibrary.Export;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;
using FinanceApplication.services;

namespace FinanceApplication.commands.category
{
    public class ExportCategoryDataCommand(
        IExportService exportService,
        CategoryProcessingFacade categoryFacade)
        : BaseExportCommand(exportService)
    {
        protected override async Task<List<IExportable>> GetDataAsync()
        {
            List<Category> categories = await categoryFacade.GetAllCategories();
            return categories.Cast<IExportable>().ToList();
        }

        protected override string GetEntityName()
        {
            return "Category";
        }
    }
}