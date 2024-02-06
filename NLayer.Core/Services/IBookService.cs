using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IBookService : IService<Book>
    {
        Task<IEnumerable<Book>> GetBorrowedBooksAsync();
        Task<IEnumerable<Book>> GetFinishedBooksAsync();

        Task SoftDeleteAsync(int id);

        Task<IEnumerable<Book>> GetSoftRemovedAllAsync();

        Task BorrowBookAsync(int bookId, int borrowerId);
        Task GiveBookToOwnerAsync(int bookId);

    }
}
