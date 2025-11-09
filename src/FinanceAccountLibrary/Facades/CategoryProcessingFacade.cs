using FinanceAccountLibrary.factories;
using FinanceAccountLibrary.models;
using FinanceAccountLibrary.models.Enums;
using FinanceAccountLibrary.repository;

namespace FinanceAccountLibrary.Facades
{
    public class CategoryProcessingFacade
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOperationRepository _operationRepository;

        public CategoryProcessingFacade(ICategoryRepository categoryRepository,
            IOperationRepository operationRepository)
        {
            _categoryRepository = categoryRepository;
            _operationRepository = operationRepository;
        }

        public async Task<Category> CreateCategory(CategoryType type, string name)
        {
            Category category = CategoryFactory.Create(type, name);
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<Category> CreateCategory(string type, string name)
        {
            Category category = CategoryFactory.Create(type, name);
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<Category> UpdateCategory(Guid id, CategoryType type, string name)
        {
            Category? category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.Type = type;
            category.Name = name;
            return await _categoryRepository.UpdateCategory(category);
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            List<Category> allCategories = await _categoryRepository.GetAllCategories();
            return allCategories.FirstOrDefault(c => c.Name == name);
        }

        public Task<List<Category>> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public async Task<List<Category>> GetCategoriesByType(CategoryType type)
        {
            List<Category> allCategories = await _categoryRepository.GetAllCategories();
            return allCategories.Where(c => c.Type == type).ToList();
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            Category? category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            if ((await _operationRepository.GetOperationsByCategoryId(id)).Any())
            {
                throw new Exception("Category has operations associated with it");
            }

            return await _categoryRepository.DeleteCategory(category);
        }
    }
}