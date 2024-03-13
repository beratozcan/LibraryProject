using NLayer.Core.Entities;


namespace NLayer.Core.Services
{
    public interface IUserRefreshTokenService : IService<UserRefreshToken>
    {
        public Task RevokeAsync(int userId);
    }
}
