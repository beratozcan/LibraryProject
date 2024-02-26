using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IUserService : IService<User>
    {
        //Task<ICollection<User>> GetUserWithBooks(int id);
        // Task<ICollection<User>> GetUsersAsync();
        // Task Remove(int id);

        public void CreateUser(string username, string password);

        public void UpdateUser(int id,string username, string password);
        Task<bool> AuthenticateUser(string username, string password);

        public bool DidUserLogin(int userId);

    }
}
