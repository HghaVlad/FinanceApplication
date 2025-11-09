using FinanceAccountLibrary.Export;

namespace FinanceApplication.services
{
    public interface IExportService
    {
        Task Export(List<IExportable> data, ExportFormat format, string filePath);
    }
}