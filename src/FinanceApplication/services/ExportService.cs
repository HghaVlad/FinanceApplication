using FinanceAccountLibrary.Export;

namespace FinanceApplication.services
{
    public class ExportService : IExportService
    {
        public async Task Export(List<IExportable> data, ExportFormat format, string filePath)
        {
            ExporterBase exporter = format switch
            {
                ExportFormat.Csv => new CsvExporter(),
                ExportFormat.Json => new JsonExporter(),
                ExportFormat.Yaml => new YamlExporter(),
                _ => throw new ArgumentException("Invalid export format")
            };
            exporter.Clear();

            foreach (IExportable entity in data)
            {
                entity.Accept(exporter);
            }

            await SaveToFile(exporter.GetContent(), filePath);
        }

        private Task SaveToFile(string content, string filePath)
        {
            File.WriteAllText(filePath, content);

            return Task.CompletedTask;
        }
    }
}