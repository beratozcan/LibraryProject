using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class GenreCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }

    public class GenreUpdateModel
    {
        [Required]

        public string Name { get; set; } = null!;
    }


}
