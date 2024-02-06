using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {

        private readonly ICategoryRepository _repository;
        

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
            : base(repository, unitOfWork)
        {
            
            _repository = categoryRepository;

        }
        public async Task<IEnumerable<Category>> GetCategoryWithBooksAsync()
        {
            return await _repository.GetCategoriesWithBooksAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _repository.SoftDeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetSoftRemovedAllAsync()
        {
            return await _repository.GetSoftRemovedAllAsync();
        }
    }
}
