﻿using System.ComponentModel.DataAnnotations;
namespace NLayer.Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
