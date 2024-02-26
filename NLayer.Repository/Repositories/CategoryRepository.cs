using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(AppDbContext context) : base(context)
        {
            


        }

        public override async Task<ICollection<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    BookCategories = c.BookCategories.Select(bc => new BookCategory
                    {
                        Book = new Book
                        {
                            Id = bc.Book.Id,
                            Name = bc.Book.Name
                        }
                    }).ToList(),
                })
                
                .ToListAsync();
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    BookCategories = c.BookCategories.Select(bc => new BookCategory
                    {
                        Book = new Book
                        {
                            Id = bc.Book.Id,
                            Name = bc.Book.Name
                        }
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
        }

    }
}
