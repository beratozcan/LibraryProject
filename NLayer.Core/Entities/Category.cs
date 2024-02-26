using NLayer.Core.Entities;
using System.ComponentModel.DataAnnotations;
namespace NLayer.Core.Models
{
    public class Category : ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; } = [];
    }
}
