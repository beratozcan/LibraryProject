using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBorrowedBooksAsync();
        Task<IEnumerable<Book>> GetFinishedBooksAsync();

        Task SoftDeleteAsync(int id);

        Task<IEnumerable<Book>> GetSoftRemovedAllAsync();

        Task ChangeOwner(int bookId, int latestOwnerId);



    }
}
