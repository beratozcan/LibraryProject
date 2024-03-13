using NLayer.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NLayer.Core.Models
{
    public class Category : ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; } = [];


        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    } 
}
