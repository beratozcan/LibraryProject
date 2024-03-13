using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Service.Exceptions;
using TokenHandler = NLayer.API.Security.TokenHandler;

namespace NLayer.Repository.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        private readonly IConfiguration _configuration;
        public UserTokenRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }
        public async Task<UserToken> GenerateTokenForUserAsync(int userId)
        {
            var _token = TokenHandler.CreateToken(_configuration);

            var token = new UserToken
            {
                UserId = userId,
                Token = _token.AccessToken,
                RefreshToken = _token.RefreshToken,
                ExpiredAt = _token.Expiration
            };

            var refreshToken = new UserRefreshToken
            {
                UserId = userId,
                Token = token.RefreshToken,
                CreatedAt = DateTime.Now,
            };

            _context.UserRefreshTokens.Add(refreshToken);

            return token;
        }
        public bool DidUserAuthenticate(HttpContext httpContext)
        {
            var tokens = httpContext.Request.Headers["Authorization"].ToString();
            var accessToken = tokens.Substring("Bearer".Length).Trim();
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == accessToken);

            if (userTokenEntity == null)
            {
                return false; 
            }

            return true;
        }
        public async Task<string> RefreshTokenAsync(string refreshToken)
        { 
            var userToken = _context.UserTokens.FirstOrDefault(t => t.RefreshToken == refreshToken);

            if (userToken != null)
            {   
                var userRefreshToken = _context.UserRefreshTokens.FirstOrDefault(r => r.UserId == userToken.UserId);

                if (userRefreshToken != null)
                {
                    var newToken = TokenHandler.CreateToken(_configuration);

                    userToken.Token = newToken.AccessToken;
                    userToken.RefreshToken = newToken.RefreshToken;
                    userToken.ExpiredAt = DateTime.Now;
                    userToken.RevokedAt = DateTime.Now;
                    userRefreshToken.RevokedAt = DateTime.Now;

                    var newRefreshToken = new UserRefreshToken
                    {
                        UserId = userToken.UserId,
                        Token = newToken.RefreshToken,
                        CreatedAt = DateTime.Now
                    };

                    _context.UserRefreshTokens.Add(newRefreshToken);
                    
                    await _context.SaveChangesAsync();

                    return userToken.Token;
                }
                else
                {
                    throw new NotFoundException("No refresh token found for the user.");
                }
            }
            else
            {
                throw new NotFoundException("Refresh token not found or expired.");
            }
        }

        public async Task RevokeTokenAsync(string token)
        {

            var _token =  _context.UserTokens.FirstOrDefault(x => x.Token == token);
            var _refreshToken = _context.UserRefreshTokens.FirstOrDefault(r => r.UserId == _token.UserId);

            if (_token != null)
            {
                _token.RevokedWith = _token.Token;
                _refreshToken.RevokedAt = DateTime.Now;

                _token.RevokedAt = DateTime.Now; 
                _context.UserTokens.Remove(_token);
                _context.UserRefreshTokens.Remove(_refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public string TakeAccessToken(HttpContext httpContext)
        {
            var tokens = httpContext.Request.Headers["Authorization"].ToString();
            var accessToken = tokens.Substring("Bearer ".Length).Trim();

            return accessToken;
        }
    }
}
