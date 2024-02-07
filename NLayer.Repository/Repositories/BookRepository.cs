using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class BookRepository(AppDbContext context, IBorrowedBooksLoggerRepository loggerRepository) : GenericRepository<Book>(context), IBookRepository
    {
        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {

            var books = await context.Books
                
                .ToListAsync();

            var borrowedBooks = books.Where(b => b.IsBorrowed == true);


            return borrowedBooks;
        }

        public async Task<IEnumerable<Book>> GetFinishedBooksAsync()
        {
            return await context.Set<Book>().Where(b => b.HaveRead).ToListAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entityToDelete = await context.Books.FindAsync(id);

            if (entityToDelete != null)
            {
                entityToDelete.IsRemoved = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetSoftRemovedAllAsync()
        {
            return await context.Set<Book>().Where(b => !b.IsRemoved).ToListAsync();
        }

        public async Task BorrowBookAsync(int bookId, int borrowerId)
        {
            var bookEntity = await context.Books.FindAsync(bookId);

            
            if(bookEntity != null) 
            {
                if(bookEntity.IsBorrowed == true)
                {
                    throw new InvalidOperationException("The book is already borrowed");
                }
                
                bookEntity.BorrowerId = borrowerId;
                bookEntity.IsBorrowed = true;
                await _context.SaveChangesAsync();

                await loggerRepository.LogBorrowedBookHistoryAsync(bookId, borrowerId);
            }

        }

        public async Task GiveBookToOwnerAsync(int bookId)
        {
            var bookEntity = await _context.Books.FindAsync(bookId);

            if(bookEntity != null)
            {
                bookEntity.IsBorrowed = null;
                bookEntity.BorrowerId = null;
                await _context.SaveChangesAsync();

            }
        }

        
    }
}
