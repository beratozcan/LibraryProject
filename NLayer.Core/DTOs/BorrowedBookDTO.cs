﻿namespace NLayer.Core.DTOs
{
    public class BorrowedBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public int Page { get; set; }
        public bool haveRead { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
