using FinanceAccountLibrary.models;
using FinanceAccountLibrary.models.Enums;

namespace FinanceAccountLibrary.factories
{
    public static class CategoryFactory
    {
        /// <summary>
        /// The method creates a new category with type and name
        /// </summary>
        /// <param name="type">Enum CategoryType</param>
        /// <param name="name">The name of new category</param>
        /// <returns>The created category</returns>
        public static Category Create(CategoryType type, string name)
        {
            return new Category(Guid.NewGuid(), type, name);
        }

        /// <summary>
        /// The method creates a category with the type specified as string(not enum)
        /// </summary>
        /// <param name="type">The type of the category Income or Outcome</param>
        /// <param name="name">The name of new category</param>
        /// <returns>The created category</returns>
        /// <exception cref="ArgumentException">The type is not valid if the type is not "Income" or "Outcome"</exception>
        public static Category Create(string type, string name)
        {
            if (type == "Income")
            {
                return new Category(Guid.NewGuid(), CategoryType.Income, name);
            }

            if (type == "Outcome")
            {
                return new Category(Guid.NewGuid(), CategoryType.Outcome, name);
            }

            throw new ArgumentException("The type is not valid", nameof(type));
        }
    }
}