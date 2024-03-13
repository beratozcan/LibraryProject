using NLayer.Core.Entities;

namespace NLayer.Core.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        public void RemoveGenre(int genreId, string token);
    }
}
