using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Entities
{
    public class BookBorrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int BorrowerId { get;set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? GiveBackTime { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; } = null!;

        [ForeignKey("BorrowerId")]
        
        [DeleteBehavior(DeleteBehavior.Restrict)] 
        public virtual User Borrower { get; set; } = null!;

    }
}
