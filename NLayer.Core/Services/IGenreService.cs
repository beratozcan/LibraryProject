using NLayer.Core.Entities;

namespace NLayer.Core.Services
{
    public interface IGenreService : IService<Genre>
    {
        public void RemoveGenre(int genreId, string token);
    }
}
