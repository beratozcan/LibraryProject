using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }

    public class CategoryCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]

        public int UserId { get; set; } 
    }

    public class CategoryUpdateModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
