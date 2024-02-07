using NLayer.Core.Entities;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class BorrowedBooksLoggerRepository : GenericRepository<BookBorrowing>, IBorrowedBooksLoggerRepository
    {
        public BorrowedBooksLoggerRepository(AppDbContext context) : base(context) 
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

        
    }
}
