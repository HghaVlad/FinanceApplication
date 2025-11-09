using FinanceAccountLibrary.Export;
using FinanceApplication.services;

namespace FinanceApplication.commands
{
    public abstract class BaseExportCommand : ICommand
    {
        protected readonly IExportService ExportService;

        protected BaseExportCommand(IExportService exportService)
        {
            ExportService = exportService;
        }


        public string Name => $"Export {GetEntityName()}";


        public async Task Execute()
        {
            Console.WriteLine("==================");
            Console.WriteLine($"Export of {GetEntityName()}");

            List<IExportable> data = await GetDataAsync();
            if (!data.Any())
            {
                Console.WriteLine("No data to export");
                return;
            }

            ExportFormat format = await SelectFormat();
            if (format == ExportFormat.None)
            {
                Console.WriteLine("Export is canceld");
                return;
            }

            string filePath = GetFilePath(format);
            try
            {
                await ExportService.Export(data, format, filePath);
                Console.WriteLine($"Export completed: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export failed: {ex.Message}");
            }
        }

        protected abstract Task<List<IExportable>> GetDataAsync();
        protected abstract string GetEntityName();


        protected virtual Task<ExportFormat> SelectFormat()
        {
            while (true)
            {
                Console.WriteLine("\nSelect export format:");
                Console.WriteLine("1. CSV");
                Console.WriteLine("2. JSON");
                Console.WriteLine("3. YAML");
                Console.WriteLine("4. Cancel");
                Console.Write("Your choice: ");

                string? input = Console.ReadLine();
                int choice;
                while (!int.TryParse(input, out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Please input digit between 1 and 4");
                    Console.Write("Your choice: ");
                    input = Console.ReadLine();
                }

                switch (choice)
                {
                    case 1:
                        return Task.FromResult(ExportFormat.Csv);
                    case 2:
                        return Task.FromResult(ExportFormat.Json);
                    case 3:
                        return Task.FromResult(ExportFormat.Yaml);
                    default:
                        return Task.FromResult(ExportFormat.None);
                }
            }
        }

        protected virtual string GetFilePath(ExportFormat format)
        {
            string extension = format switch
            {
                ExportFormat.Csv => "csv",
                ExportFormat.Json => "json",
                ExportFormat.Yaml => "yaml",
                _ => "txt"
            };

            Console.Write($"Enter file path (e.g., export/data.{extension}): ");
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input)
                ? Path.Combine(CurrentDirectory, $"export_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}")
                : GetCorrectPath(input);
        }

        private string GetCorrectPath(string inputPath)
        {
            if (Path.IsPathFullyQualified(inputPath))
            {
                return inputPath;
            }

            return Path.Combine(CurrentDirectory, inputPath);
        }


        private static string CurrentDirectory
        {
            get
            {
                string[] dirs = Directory.GetCurrentDirectory().Split(Path.DirectorySeparatorChar);
                return string.Join(Path.DirectorySeparatorChar, dirs, 0, dirs.Length - 3)
                       + Path.DirectorySeparatorChar;
            }
        }
    }
}