using NLayer.Core.Entities;
using NLayer.Core.Models;

namespace NLayer.Service.Mappers
{
    public class GenreMapper
    {
        public static Genre ToEntity(GenreCreateModel model)
        {
            return new Genre
            {
                Name = model.Name
               
            };
        }

        public static Genre ToEntity(GenreUpdateModel model, Genre entity)
        {
            entity.Name = model.Name;

            return entity;

        }
        public static GenreViewModel ToViewModel(Genre genre)
        {
            return new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };
            

        }
        public static List<GenreViewModel> ToViewModelList(IEnumerable<Genre> genres)
        {
            return genres.Select(ToViewModel).ToList();

        }

    }
}
