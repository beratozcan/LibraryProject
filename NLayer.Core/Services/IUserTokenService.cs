using Microsoft.AspNetCore.Http;
using NLayer.Core.Entities;

namespace NLayer.Core.Services
{
    public interface IUserTokenService : IService<UserToken>
    {
        public Task<UserToken> GenerateTokenForUserAsync(int userId);

        public Task RevokeTokenAsync(string token);

        public Task<string> RefreshTokenAsync(string token);

        public bool DidUserAuthenticate(HttpContext httpContext);

        public string TakeAccessToken(HttpContext httpContext);
    }
}
