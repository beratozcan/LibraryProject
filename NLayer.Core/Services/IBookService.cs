using NLayer.Core.Entities;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IBookService : IService<Book>
    {
        Task<ICollection<Book>> GetBooksByStatus(int status);
        Task BorrowBookAsync(int bookId, int borrowerId);
        Task GiveBookToOwnerAsync(int bookId);

       // Task<ICollection<Book>> GetBooksAsync();
        // Task Remove(int id);

        Task AddBookToCategoryAsync(int bookId, int categoryId);

        public bool DoesUserHaveBook(int userId, int bookId);




    }
}
