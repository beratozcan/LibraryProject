using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        Task<IEnumerable<Category>> GetCategoriesWithBooksAsync();

        Task SoftDeleteAsync(int id);

        Task<IEnumerable<Category>> GetSoftRemovedAllAsync();
    }
}
