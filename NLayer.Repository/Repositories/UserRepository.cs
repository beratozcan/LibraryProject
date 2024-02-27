using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System.Net.NetworkInformation;

namespace NLayer.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) 
        {        
        
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return false;
                
            }

            if (PasswordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                user.DidLogin = true;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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

        public bool DidUserLogin(int userId)
        {
            var userEntity = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                throw new Exception("Kullanici bulunamadi");
            }

            return userEntity.DidLogin;
        }



        public override async Task<ICollection<User>> GetAllAsync()
        {
            
            var usersWithBooks = await _context.Users
                                    .Include(user => user.OwnedBooks)
                                    .ToListAsync();
            return usersWithBooks;
        }

        public void UpdateUser(int id,string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            byte[] salt = PasswordHasher.GenerateSalt();
            byte[] hashedPassword = PasswordHasher.HashPassword(password, salt);

            if(user == null)
            {
                throw new Exception("Kullanici bulunamadi");
            }
            
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            user.UserName = username;

            _context.SaveChanges();



        }


    }
}
