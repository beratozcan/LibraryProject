using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs
{
    public class UserModel
    {
      
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }

    public class UserPostModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class UserPutModel
    {

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}
