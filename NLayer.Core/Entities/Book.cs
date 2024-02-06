using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public int Page {  get; set; }
        public bool HaveRead { get; set; }

        [NotMapped]
        public bool? IsBorrowed
        { get { return BorrowerId != null; }
          set { }
        }

        [Required]
        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int OwnerId { get; set; }
        
        public int? BorrowerId { get; set; }

        public bool IsRemoved { get; set; }
        
    }
}
