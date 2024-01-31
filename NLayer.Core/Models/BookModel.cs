using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
    public class BookModel
    {
        public int Id { get; set; }
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
        public bool HaveRead { get; set; }
        [Required]
        public bool IsBorrowed { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class BookPostModel
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
        public bool HaveRead { get; set; }
        [Required]
        public bool IsBorrowed { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class BorrowedBookModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Required]
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public bool HaveRead { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class FinishedBookModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Required]
        public string Publisher { get; set; } = null!;

        public DateTime PublishDate { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public bool IsBorrowed { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
