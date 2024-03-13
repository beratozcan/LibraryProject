using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public void RemoveCategory(int categoryId, string token);
 
    }
}
