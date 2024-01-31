using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Models
{
    public class User
    {
        
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public List<Book> Books {  get; set; }
    }
}
