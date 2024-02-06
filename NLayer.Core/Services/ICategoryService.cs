using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<IEnumerable<Category>> GetCategoryWithBooksAsync();

        Task SoftDeleteAsync(int id);

        Task<IEnumerable<Category>> GetSoftRemovedAllAsync();
    }
}
