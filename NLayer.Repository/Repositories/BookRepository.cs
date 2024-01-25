using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;


namespace NLayer.Repository.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context): base(context) 
        {
                
        } 
        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _context.Set<Book>().Where(b => b.isBorrowed).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetFinishedBooksAsync()
        {
            return await _context.Set<Book>().Where(b => b.haveRead).ToListAsync();
        }
    }
}
