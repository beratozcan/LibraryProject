using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {

        private readonly ICategoryRepository _repository;
        

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
            : base(categoryRepository, unitOfWork)
        {
            
            _repository = categoryRepository;

        }

    }
}
