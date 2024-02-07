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

        public BorrowedBooksLoggerService(IGenericRepository<BookBorrowing> repository, IUnitOfWork unitOfWork, IBorrowedBooksLoggerRepository borrowedLogrepository)
            :base(repository,unitOfWork)
        {
            _repository = borrowedLogrepository;
        }
        public async Task LogBorrowedBookHistoryAsync(int bookId, int borrowerId)
        {
            await _repository.LogBorrowedBookHistoryAsync(bookId,borrowerId);
        }
    }
}
