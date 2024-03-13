using NLayer.Core.Entities;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IAuthenticateService : IService<User>
    {
        public Task<UserToken> AuthenticateUser(string username, string password);
    }
}
