using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public override async Task<ICollection<Category>> GetAllAsync(string token)
        {

            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);
            var _categories = await _context.Categories
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                .Where(c => c.UserId == userTokenEntity.UserId).ToListAsync();

            return _categories;
        }

        public override async Task<Category> GetByIdAsync(int id, string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            var categoryEntity = await _context.Categories
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                .FirstOrDefaultAsync(c => c.UserId == userTokenEntity.UserId && c.Id == id)
                ?? throw new UnauthorizedAccessException("Kullanici boyle bir categorye sahip degil");

            return categoryEntity;
        }

        public void RemoveCategory(int categoryId, string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);
            var categoryEntity = _context.Categories.FirstOrDefault(c => c.Id == categoryId && c.UserId == userTokenEntity.UserId)
                                                    ?? throw new UnauthorizedAccessException("Kullanici boyle bir categorye sahip degil"); ;

            categoryEntity.IsDeleted = true;
            _context.SaveChanges();

        }
    }
}
