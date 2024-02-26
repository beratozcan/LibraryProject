using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class GenreService : Service<Genre>, IGenreService
    {

        private readonly IGenreRepository _repository;

        public GenreService(IUnitOfWork unitOfWork, IGenreRepository genreRepository)
            :base(genreRepository, unitOfWork)
        {
            _repository = genreRepository;
        }

    }
}
