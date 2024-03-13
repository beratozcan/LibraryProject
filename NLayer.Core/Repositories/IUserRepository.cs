using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    { 
        public void CreateUser(string username, string password);
        public void UpdateUser(int id,string username, string password);

        public int GetAuthenticatedUserId(string token);

        public void RemoveUser(int id,string token);
    }
}
