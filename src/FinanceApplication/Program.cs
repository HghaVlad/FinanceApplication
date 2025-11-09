using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.repository;
using FinanceApplication.Factories;
using Microsoft.Extensions.DependencyInjection;
using FinanceApplication.Repositories.sqlite;
using FinanceApplication.services;
using Microsoft.EntityFrameworkCore;


namespace FinanceApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = $"{CurrentDirectory}/finance.db";
            string logFilePath = $"{CurrentDirectory}/logs.txt";

            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<FinanceDbContext>(options =>
                options.UseSqlite($"Data Source={connectionString}"));


            services.AddScoped<IBankAccountRepository, SqliteBankAccountRepository>();
            services.AddScoped<ICategoryRepository, SqliteCategoryRepository>();
            services.AddScoped<IOperationRepository, SqliteOperationRepository>();

            services.AddScoped<BankAccountProcessingFacade>();
            services.AddScoped<CategoryProcessingFacade>();
            services.AddScoped<OperationProcessingFacade>();

            services.AddSingleton(new CommandLogger(logFilePath));
            services.AddScoped<CommandDecoratorFactory>();
            services.AddScoped<IExportService, ExportService>();

            services.AddScoped<Application>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();


            using IServiceScope scope = serviceProvider.CreateScope();
            FinanceDbContext dbContext = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
            dbContext.Database.EnsureCreated();


            Application app = scope.ServiceProvider.GetRequiredService<Application>();
            app.Run();
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