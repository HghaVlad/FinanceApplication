using FinanceAccountLibrary.models;

namespace FinanceAccountLibrary.repository
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category?> GetCategoryById(Guid id);
        Task<bool> DeleteCategory(Category category);
        Task<List<Category>> GetAllCategories();
    }
}