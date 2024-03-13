using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class BookService : Service<Book>, IBookService
    {

        private readonly IBookRepository _repository;
        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository, IUserTokenRepository userTokenRepository)
            :base(bookRepository,unitOfWork)
        {
            
            _repository = bookRepository;
        }

        public Task AddBookToBorrowedBooksAsync(int bookId, string token)
        {
            return _repository.AddBookToBorrowedBooksAsync(bookId, token);
        }

        public async Task AddBookToCategoryAsync(int bookId, int categoryId, string token)
        {
            await _repository.AddBookToCategoryAsync(bookId, categoryId,token);
        }
        public async Task BorrowBookAsync(int bookId, string token)
        {
            await _repository.BorrowBookAsync(bookId, token);
        }

        public async Task<ICollection<Book>> GetBooksByStatus(int status, string token)
        {
            return await _repository.GetBooksByStatus(status,token);
        }

        public async Task GiveBookToOwnerAsync(int bookId, string token)
        {
            await _repository.GiveBookToOwnerAsync(bookId, token);
        }

        public Task RemoveBookFromCategoryAsync(int bookId, int categoryId, string token)
        {
            return _repository.RemoveBookFromCategoryAsync(bookId,categoryId,token);
        }

        public Task RemoveBorrowedBookAsync(int bookId, string token)
        {
            return _repository.RemoveBorrowedBookAsync(bookId,token);
        }
    }
}
