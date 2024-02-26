using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();

    }

    public class UserCreateModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class UserUpdateModel
    {
        

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }