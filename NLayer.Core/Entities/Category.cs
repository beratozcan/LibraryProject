using System.ComponentModel.DataAnnotations;
namespace NLayer.Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public List<Book> Books { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool IsRemoved { get; set; }
    }
}
