using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.Entities
{
    public class Genre : ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
