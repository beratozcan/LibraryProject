using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        public List<Book> Books {  get; set; }
    }
}
