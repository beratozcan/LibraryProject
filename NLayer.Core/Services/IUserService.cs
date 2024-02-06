using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IUserService : IService<User>
    {
        Task<IEnumerable<User>> GetUserWithBooks(int id);
    }
}
