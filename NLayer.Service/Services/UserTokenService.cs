using Microsoft.AspNetCore.Http;
using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class UserTokenService : Service<UserToken>, IUserTokenService
    {
        private readonly IUserTokenRepository _repository;

        public UserTokenService(IUnitOfWork unitOfWork, IUserTokenRepository userTokenRepository)
            : base(userTokenRepository, unitOfWork)
        {
            _repository = userTokenRepository;
        }

        public bool DidUserAuthenticate(HttpContext httpContext)
        {
            return _repository.DidUserAuthenticate(httpContext);
        }

        public Task<UserToken> GenerateTokenForUserAsync(int userId)
        {
            return _repository.GenerateTokenForUserAsync(userId);
        }

        public Task<string> RefreshTokenAsync(string token)
        {
            return _repository.RefreshTokenAsync(token);
        }

        public Task RevokeTokenAsync(string token)
        {
            return _repository.RevokeTokenAsync(token);
        }

        public string TakeAccessToken(HttpContext httpContext)
        {
            return _repository.TakeAccessToken(httpContext);
        }
    }
}
