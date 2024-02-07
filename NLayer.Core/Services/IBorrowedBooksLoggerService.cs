using NLayer.Core.Entities;

namespace NLayer.Core.Services
{
    public interface IBorrowedBooksLoggerService : IService<BookBorrowing>
    {
        Task LogBorrowedBookHistoryAsync(int bookId, int borrowerId);
    }
}
