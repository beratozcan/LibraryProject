using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Service.Exceptions;

namespace NLayer.Repository.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
            
        }

        public void RemoveGenre(int genreId, string token)
        {
            var userEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);
            var genreEntity = _context.Genres.FirstOrDefault(g => g.Id == genreId);
            var doesUserHaveGenre = _context.Books.Where(b => b.OwnerId == userEntity.UserId)
                                                   .Any(b => b.GenreId == genreId);

            if (genreEntity == null)
            {
                throw new NotFoundException("Boyle bir genre yok");
            }

            else if (!doesUserHaveGenre)
            {
                throw new UnauthorizedAccessException("Kullanicinin boyle bir genresi yok");

            }
            else
            {
                genreEntity.IsDeleted = true;
                _context.SaveChanges();

            }
        }
            public override async Task<Genre> GetByIdAsync(int id, string token)
            {
                var genreEntity =  await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

                if (genreEntity == null)
                {
                  throw new NotFoundException("Boyle bir genre yok");

                }
                return genreEntity;
           }

            public override async Task<ICollection<Genre>> GetAllAsync(string token)
            {
                var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

                var genres = await _context.Genres.ToListAsync();

                return genres;
            }
    }
}
