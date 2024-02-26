using NLayer.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayer.Core.Entities
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }
        
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
