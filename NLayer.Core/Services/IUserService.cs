using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IUserService : IService<User>
    {
        public void CreateUser(string username, string password);

        public void UpdateUser(int id,string username, string password);

        public int GetAuthenticatedUserId(string token);

        public void RemoveUser(int id,string token);
    }
}
