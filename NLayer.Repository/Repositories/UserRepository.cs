using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) 
        {        
        
        }
        public async Task<IEnumerable<User>> GetUserWithBooks(int id)
        {

            var userWithBooks = await _context.Users
                .Include(user => user.Books )
                .Where(user => user.Id == id)
                .ToListAsync();

            return userWithBooks;
        }
    }
}
