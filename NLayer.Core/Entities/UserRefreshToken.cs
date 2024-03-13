using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Entities
{
    public class UserRefreshToken
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId {  get; set; }

        [Required]

        public string Token { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
