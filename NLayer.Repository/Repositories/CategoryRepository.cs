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
        public async Task<IEnumerable<Category>> GetCategoriesWithBooksAsync()
        {
            var categoriesWithBooks = await _context.Categories
            .Include(category => category.Books)  
            .ToListAsync();

            return categoriesWithBooks;
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entityToDelete = await _context.Categories.FindAsync(id);

            if (entityToDelete != null)
            {
                entityToDelete.IsRemoved = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetSoftRemovedAllAsync()
        {
            return await _context.Set<Category>().Where(b => !b.IsRemoved).ToListAsync();
        }
    }
}
