﻿namespace NLayer.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public int Page {  get; set; }
        public bool haveRead { get; set; }
        public bool isBorrowed { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
