using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;


namespace NLayer.Service.Services
{
    public class BookService : Service<Book>, IBookService
    {

        private readonly IBookRepository _repository;
        
        public BookService(IGenericRepository<Book> repository,IUnitOfWork unitOfWork, IBookRepository bookRepository)
            :base(repository,unitOfWork)
        {
            
            _repository = bookRepository;
            
        }
        
        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _repository.GetBorrowedBooksAsync();
        }

        public async Task<IEnumerable<Book>> GetFinishedBooksAsync()
        {
            return await _repository.GetFinishedBooksAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _repository.SoftDeleteAsync(id);
        }

        public async Task<IEnumerable<Book>> GetSoftRemovedAllAsync()
        {
           return  await _repository.GetSoftRemovedAllAsync();
        }

        public async Task BorrowBookAsync(int bookId, int borrowerId)
        {
            await _repository.BorrowBookAsync(bookId,borrowerId);
        }

        public async Task GiveBookToOwnerAsync(int bookId)
        {
            await _repository.GiveBookToOwnerAsync(bookId);
        }

        
    }
}
