using NLayer.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Models
{
    public class User : ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        
        public byte[] PasswordHash { get; set; } = null!;

        [Required]
        
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual ICollection<Category>? Categories { get; set; }

        public virtual ICollection<Book>? OwnedBooks {get; set;}

        public virtual ICollection<Book>? BorrowedBooks { get; set; }

        public bool IsDeleted { get; set; }
    }
}
