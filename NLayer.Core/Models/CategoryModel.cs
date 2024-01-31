using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; } = null!;
    }

    public class CategoryPostModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
