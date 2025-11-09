using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models;

namespace FinanceApplication.commands.category
{
    public class ListCategoriesCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;

        public ListCategoriesCommand(CategoryProcessingFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public string Name => "List all categories";

        public async Task Execute()
        {
            Console.WriteLine("All Categories:");
            Console.WriteLine("================");
            try
            {
                List<Category> categories = await _categoryFacade.GetAllCategories();

                if (categories.Any())
                {
                    foreach (Category category in categories)
                    {
                        Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Type: {category.Type}");
                    }
                }
                else
                {
                    Console.WriteLine("No categories found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing categories: {ex.Message}");
            }
        }
    }
}