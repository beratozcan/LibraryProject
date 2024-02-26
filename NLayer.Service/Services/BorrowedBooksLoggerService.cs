using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
