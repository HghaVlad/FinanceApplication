using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models.Enums;

namespace FinanceApplication.commands.category
{
    public class UpdateCategoryCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;

        public UpdateCategoryCommand(CategoryProcessingFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public string Name => "Update category";

        public async Task Execute()
        {
            Console.WriteLine("Update Category:");
            Console.WriteLine("================");
            try
            {
                Console.WriteLine("Enter category ID to update:");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    Category? category = await _categoryFacade.GetCategoryById(id);
                    if (category != null)
                    {
                        Console.WriteLine($"Current category: {category.Name} ({category.Type})");

                        // Update name
                        Console.WriteLine($"Enter new name (current: {category.Name}) or press Enter to keep current:");
                        string? nameInput = Console.ReadLine();
                        string newName = string.IsNullOrWhiteSpace(nameInput) ? category.Name : nameInput;

                        // Update type
                        Console.WriteLine($"Enter new type (current: {category.Type}) or press Enter to keep current:");
                        Console.WriteLine("Available types: Income, Outcome");
                        string? typeInput = Console.ReadLine();
                        CategoryType newType = category.Type;
                        if (!string.IsNullOrWhiteSpace(typeInput) &&
                            Enum.TryParse(typeInput, true, out CategoryType parsedType))
                        {
                            newType = parsedType;
                        }

                        Category updatedCategory = await _categoryFacade.UpdateCategory(id, newType, newName);
                        Console.WriteLine($"Category updated successfully!");
                        Console.WriteLine(
                            $"ID: {updatedCategory.Id}, Name: {updatedCategory.Name}, Type: {updatedCategory.Type}");
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
                Console.WriteLine($"Error updating category: {ex.Message}");
            }
        }
    }
}