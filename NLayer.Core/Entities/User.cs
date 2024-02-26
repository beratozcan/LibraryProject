using NLayer.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]

        public bool DidLogin { get; set; } 

        public virtual ICollection<Book>? OwnedBooks {get; set;}

        public virtual ICollection<Book>? BorrowedBooks { get; set; }

        public bool IsDeleted { get; set; }
    }
}
