using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IBookService : IService<Book>
    {
        Task<IEnumerable<Book>> GetBorrowedBooksAsync();
        Task<IEnumerable<Book>> GetFinishedBooksAsync();
    }
}
