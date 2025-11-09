using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;

namespace FinanceApplication.commands.category
{
    public class DeleteCategoryCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;

        public DeleteCategoryCommand(CategoryProcessingFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public string Name => "Delete category";

        public async Task Execute()
        {
            Console.WriteLine("Delete Category:");
            Console.WriteLine("================");
            
            try
            {
                Console.WriteLine("Enter category ID to delete:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {

                    Category? category = await _categoryFacade.GetCategoryById(id);
                    if (category != null)
                    {
                        Console.WriteLine($"Category found: {category.Name} ({category.Type})");
                        Console.WriteLine("Are you sure you want to delete this category? (y/N)");
                        
                        string? confirmation = Console.ReadLine();
                        if (confirmation?.ToLower() == "y")
                        {
                            bool result = await _categoryFacade.DeleteCategory(id);
                            if (result)
                            {
                                Console.WriteLine("Category deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete category.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Category deletion cancelled.");
                        }
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
                Console.WriteLine($"Error deleting category: {ex.Message}");
            }
        }
    }
}
