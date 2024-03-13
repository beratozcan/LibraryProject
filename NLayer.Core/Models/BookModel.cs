using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
        public class BookViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
 
            public string Author { get; set; } = null!;

            public string Publisher { get; set; } = null!;

            public DateTime PublishDate { get; set; }

            public int Page { get; set; }

            public int BookStatusId { get; set; }
    
            public int GenreId { get; set; }

            public int? BorrowerId { get; set; }

            public int OwnerId { get; set; }  
    }

    public class BookCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string Publisher { get; set; } = null!;

        [Required]
        public int Page { get; set; }

        [Required]
        public int BookStatusId { get; set; }

        [Required]
        public int GenreId { get; set; }  
        
    }

    public class BookUpdateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string Publisher { get; set; } = null!;

        [Required]
        public DateTime PublishDate { get; set; }

        public int Page { get; set; }

        [Required]
        public int BookStatusId { get; set; }


        [Required]
        public int GenreId { get; set; }
    }
    public class BookViewWithCategoriesModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public DateTime PublishDate { get; set; }

        public int Page { get; set; }

        public int BookStatusId { get; set; }

        public List<CategoryViewModel?> Categories { get; set; } = [];

        public int GenreId { get; set; }

        public int? BorrowerId { get; set; }

        public int OwnerId { get; set; }
    }

}

    

