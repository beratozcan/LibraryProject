using NLayer.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Models
{
    public class Book : ISoftDeletable
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

        [Required]
        public int Page { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public int? BorrowerId { get; set; }

        [Required]
        public int BookStatusId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual BookStatus? BookStatus { get; set; }

        // [InverseProperty("Books")]
        public virtual User? Borrower { get; set; }

        public virtual ICollection<BookCategory>? BookCategories { get; set; }

        public virtual User? Owner { get; set; }

        public virtual Genre? Genre { get; set; }
    }
}
