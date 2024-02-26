using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        Task<ICollection<Book>> GetBooksByStatus(int status);
        Task BorrowBookAsync(int bookId, int borrowerId);
        Task GiveBookToOwnerAsync(int bookId);

        // Task<ICollection<Book>> GetBooksAsync();
       // Task Remove(int id);

        Task AddBookToCategoryAsync(int bookId, int categoryId);

        


    }
}
