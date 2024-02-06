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
            return await _context.Set<Book>().Where(b =>b.IsBorrowed).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetFinishedBooksAsync()
        {
            return await _context.Set<Book>().Where(b => b.HaveRead).ToListAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entityToDelete = await _context.Books.FindAsync(id);

            if (entityToDelete != null)
            {
                entityToDelete.IsRemoved = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetSoftRemovedAllAsync()
        {
            return await _context.Set<Book>().Where(b => !b.IsRemoved).ToListAsync();
        }

        public async Task ChangeOwner(int bookId, int latestOwnerId)
        {
            var bookEntity = await _context.Books.FindAsync(bookId);
            
            if(bookEntity != null) 
            {
                bookEntity.UserId = latestOwnerId;
                bookEntity.BorrowedUserId = latestOwnerId;
                bookEntity.IsBorrowed = true;
                await _context.SaveChangesAsync();
            }

        }
    }
}
