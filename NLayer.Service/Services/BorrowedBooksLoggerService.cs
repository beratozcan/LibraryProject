using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class BorrowedBooksLoggerService : Service<BookBorrowing>, IBorrowedBooksLoggerService
    {
        private readonly IBorrowedBooksLoggerRepository _repository;

        public BorrowedBooksLoggerService( IUnitOfWork unitOfWork, IBorrowedBooksLoggerRepository borrowedLogrepository)
            :base(borrowedLogrepository, unitOfWork)
        {
            _repository = borrowedLogrepository;
        }
        public async Task LogBorrowedBookHistoryAsync(int bookId, int borrowerId)
        {
            await _repository.LogBorrowedBookHistoryAsync(bookId,borrowerId);
        }
        public async Task LogGiveBackBookHistoryAsync(int bookId)
        {
            await _repository.LogGiveBackBookHistoryAsync(bookId);
        }
    }
}
