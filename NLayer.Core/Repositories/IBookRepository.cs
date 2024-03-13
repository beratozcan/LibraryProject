using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        Task<ICollection<Book>> GetBooksByStatus(int status, string token);

        Task BorrowBookAsync(int bookId, string token);

        Task GiveBookToOwnerAsync(int bookId, string token);

        Task AddBookToCategoryAsync(int bookId, int categoryId, string token);

        Task RemoveBorrowedBookAsync(int bookId,string token);

        Task AddBookToBorrowedBooksAsync(int bookId,string token);

        Task RemoveBookFromCategoryAsync(int bookId, int categoryId, string token);

    }
}
