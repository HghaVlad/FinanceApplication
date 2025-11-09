using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;

namespace FinanceApplication.commands.category
{
    public class ShowCategoryCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;

        public ShowCategoryCommand(CategoryProcessingFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public string Name => "Show category by ID";

        public async Task Execute()
        {
            Console.WriteLine("Show Category:");
            Console.WriteLine("===============");
            try
            {
                Console.WriteLine("Enter category ID:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    Category? category = await _categoryFacade.GetCategoryById(id);
                    if (category != null)
                    {
                        Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Type: {category.Type}");
                    }
                    else
                    {
                        Console.WriteLine("Category not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading category: {ex.Message}");
            }
        }
    }
}