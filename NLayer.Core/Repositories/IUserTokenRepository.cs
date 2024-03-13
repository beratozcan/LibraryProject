using Microsoft.AspNetCore.Http;
using NLayer.Core.Entities;

namespace NLayer.Core.Repositories
{
    public interface IUserTokenRepository : IGenericRepository<UserToken>
    {
        public Task<UserToken> GenerateTokenForUserAsync(int userId);

        public Task RevokeTokenAsync(string token);

        public Task<string> RefreshTokenAsync(string token);

        public bool DidUserAuthenticate(HttpContext httpContext); 

        public string TakeAccessToken(HttpContext httpContext); 
    
    }
}
