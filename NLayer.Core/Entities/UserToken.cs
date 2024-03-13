using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Entities
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]

        public string Token { get; set; } = null!;

        [Required]

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpiredAt { get; set; }

        public DateTime? RevokedAt { get; set; } 

        public string? RevokedWith { get; set; } 

        public virtual User? User { get; set; }
    }
}
