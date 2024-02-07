using NLayer.Core.Entities;

namespace NLayer.Core.Repositories
{
    public interface IBorrowedBooksLoggerRepository : IGenericRepository<BookBorrowing>
    {
        Task LogBorrowedBookHistoryAsync(int bookId, int borrowerId);

    }
}
