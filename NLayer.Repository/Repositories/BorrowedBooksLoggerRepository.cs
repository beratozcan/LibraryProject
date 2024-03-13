using NLayer.Core.Entities;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class BorrowedBooksLoggerRepository : GenericRepository<BookBorrowing>, IBorrowedBooksLoggerRepository
    {
        public BorrowedBooksLoggerRepository(AppDbContext context, IUserTokenRepository userTokenRepository) : base(context) 
        {
        }

        public async Task LogBorrowedBookHistoryAsync(int bookId, int borrowerId)
        {
            var logger = new BookBorrowing
            {
                BookId = bookId,
                BorrowerId = borrowerId,
                BorrowDate = DateTime.Now,
            };

            _context.BorrowingLogs.Add(logger);
            await _context.SaveChangesAsync();
        }

        public async Task LogGiveBackBookHistoryAsync(int bookId)
        {
            
            var bookEntity = _context.BorrowingLogs
                                     .FirstOrDefault(b =>  b.BookId == bookId);

            if(bookEntity != null)
            {
                bookEntity.GiveBackTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();

        }
    }
}
