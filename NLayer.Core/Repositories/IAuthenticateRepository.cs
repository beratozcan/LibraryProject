using NLayer.Core.Entities;
using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IAuthenticateRepository : IGenericRepository<User>
    {
        public Task<UserToken> AuthenticateUser(string username,string password);
    }
}
