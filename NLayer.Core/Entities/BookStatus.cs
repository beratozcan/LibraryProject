using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Entities
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
