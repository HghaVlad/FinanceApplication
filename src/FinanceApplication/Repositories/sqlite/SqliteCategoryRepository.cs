using FinanceAccountLibrary.models;
using FinanceAccountLibrary.repository;
using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Repositories.sqlite
{
    public class SqliteCategoryRepository : ICategoryRepository
    {
        private readonly FinanceDbContext _context;

        public SqliteCategoryRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}