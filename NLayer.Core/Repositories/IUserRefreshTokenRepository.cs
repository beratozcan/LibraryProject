using NLayer.Core.Entities;

namespace NLayer.Core.Repositories
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        public Task RevokeAsync(int userId);
    }
}
