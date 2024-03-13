using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        
        public UserRepository(AppDbContext context) : base(context) 
        {        
        
        }

        public void CreateUser(string username, string password)
        {
            if(_context.Users.Any(u => u.UserName == username))
            {
                throw new Exception("Bu username zaten kayitli");
            }

            byte[] salt = PasswordHasher.GenerateSalt();
            byte[] hashedPassword = PasswordHasher.HashPassword(password, salt);

            var user = new User
            {
                UserName = username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public override async Task<ICollection<User>> GetAllAsync(string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            var usersWithBooks = await _context.Users
                .Include(user => user.OwnedBooks)
                 .Where(user => user.Id == userTokenEntity.UserId)
                .ToListAsync();

            return usersWithBooks;
        }

        public int GetAuthenticatedUserId(string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            return userTokenEntity.UserId;
        }

        public void RemoveUser(int id,string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token && u.UserId == id)
                                                     ?? throw new UnauthorizedAccessException("Buna izniniz yok");

            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userTokenEntity.UserId);

            userEntity.IsDeleted = true;
              _context.SaveChanges();
            
        }
        public void UpdateUser(int id,string username, string password)
        {
            var userEntity = _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new UnauthorizedAccessException("Bu islem icin izniniz yok");

            byte[] salt = PasswordHasher.GenerateSalt();
            byte[] hashedPassword = PasswordHasher.HashPassword(password, salt);

            userEntity.PasswordHash = hashedPassword;
            userEntity.PasswordSalt = salt;
            userEntity.UserName = username;

            _context.SaveChanges();
        }

        public override async Task<User> GetByIdAsync(int id, string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            if (userTokenEntity == null || userTokenEntity.UserId != id)
            {
                throw new UnauthorizedAccessException("Bu kullaniciyi goruntulemeye izniniz yok");
            }

            var userWithBooks = await _context.Users
                .Include(user => user.OwnedBooks)
                .FirstOrDefaultAsync(user => user.Id == id);

            return userWithBooks;
        }

    }
}
