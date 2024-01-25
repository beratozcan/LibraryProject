using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBorrowedBooksAsync();
        Task<IEnumerable<Book>> GetFinishedBooksAsync();
    }
}
