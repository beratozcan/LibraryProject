using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class AuthenticateRepository : GenericRepository<User>, IAuthenticateRepository
    {
        IUserTokenRepository _tokenRepository;

        public AuthenticateRepository(AppDbContext context, IUserTokenRepository tokenRepository) : base(context)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<UserToken> AuthenticateUser(string username, string password)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (userEntity == null)
            {
                throw new Exception("Authentication failed");
            }

            var verifiedPassword = PasswordHasher.VerifyPassword(password, userEntity.PasswordSalt, userEntity.PasswordHash);

            if(verifiedPassword == password)
            {

                var userTokenEntity = await _tokenRepository.GenerateTokenForUserAsync(userEntity.Id);

                _context.UserTokens.Add(userTokenEntity);

                await _context.SaveChangesAsync();

                return userTokenEntity;

            }
            else
            {
                throw new Exception("Authentication basarili degil");
            }
            
        }
    }
}
