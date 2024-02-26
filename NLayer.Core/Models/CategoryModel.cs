
using NLayer.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
      

    }

    public class CategoryCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        
    }

    public class CategoryUpdateModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }

     
    }

    public class CategoryWithBooksViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public List<BookViewModel> Books { get; set; } = [];

    }



