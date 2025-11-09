using FinanceAccountLibrary.models;
using FinanceAccountLibrary.Facades;
using FinanceAccountLibrary.models.Enums;

namespace FinanceApplication.commands.category
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly CategoryProcessingFacade _categoryFacade;

        public CreateCategoryCommand(CategoryProcessingFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public string Name => "Create new category";

        public async Task Execute()
        {
            Console.WriteLine("New Category:");
            Console.WriteLine("================");
            try
            {
                Console.Write("Enter category name: ");
                string? name = Console.ReadLine();
                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("The name can't be null or empty please write it");
                    Console.Write("Enter category name: ");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Enter category type (Income/Outcome):");
                string? typeInput = Console.ReadLine();

                if (Enum.TryParse(typeInput, true, out CategoryType type))
                {
                    Category category = await _categoryFacade.CreateCategory(type, name);
                    Console.WriteLine($"Category created successfully with ID: {category.Id}");
                }
                else
                {
                    Console.WriteLine("Invalid category type. Please enter 'Income' or 'Outcome'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
            }
        }
    }
}