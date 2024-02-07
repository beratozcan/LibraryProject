using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Entities
{
    public class BookBorrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]

        public User Borrower { get; set; }

        [Required]

        public int BorrowerId { get;set; }

        [Required]

        public DateTime BorrowDate { get; set; } 

    }
}
