using NLayer.Core.Entities;
using NLayer.Core.Models;
using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Task<ICollection<User>> GetUserWithBooksAsync(int id);

        // Task<ICollection<User>> GetUsersAsync();

        // Task Remove(int id);

        public void CreateUser(string username, string password);
        public void UpdateUser(int id,string username, string password);
        Task<bool> AuthenticateUser(string username, string password);

        public bool DidUserLogin(int userId);


    }
}
