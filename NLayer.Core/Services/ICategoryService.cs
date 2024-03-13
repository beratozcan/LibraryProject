using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        public void RemoveCategory(int categoryId, string token);
    }
}
