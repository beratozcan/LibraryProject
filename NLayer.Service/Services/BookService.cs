using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class BookService : Service<Book>, IBookService
    {

        private readonly IBookRepository _repository;
        
        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository)
            :base(bookRepository,unitOfWork)
        {
            
            _repository = bookRepository;
            
        }

        public async Task AddBookToCategoryAsync(int bookId, int categoryId)
        {
            await _repository.AddBookToCategoryAsync(bookId, categoryId);
        }

        public async Task BorrowBookAsync(int bookId, int borrowerId)
        {
            await _repository.BorrowBookAsync(bookId, borrowerId);
        }

        

        public bool DoesUserHaveBook(int userId, int bookId)
        {
            return _repository.DoesUserHaveBook(userId, bookId);
        }

        public async Task<ICollection<Book>> GetBooksByStatus(int status)
        {
            return await _repository.GetBooksByStatus(status);
        }

        
        public async Task GiveBookToOwnerAsync(int bookId)
        {
            await _repository.GiveBookToOwnerAsync(bookId);
        }

       
    }
}
