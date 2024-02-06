using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mappers;
public static class BookMapper
{
    public static Book ToEntity(BookCreateModel model)
    {
        return new Book
        {
            Name = model.Name,
            Author = model.Author,
            Publisher = model.Publisher,
            Page = model.Page,
            HaveRead = model.HaveRead,
            IsBorrowed = model.IsBorrowed,
            CategoryId = model.CategoryId,
            UserId = model.UserId
           
        };
    }

    public static Book ToEntity(BookUpdateModel model, Book entity)
    {
        entity.Name = model.Name ?? entity.Name;
        entity.Author = model.Author ?? entity.Author;
        entity.Publisher = model.Publisher ?? entity.Publisher;
        entity.Page = model.Page;
        entity.IsBorrowed = model.IsBorrowed;
        entity.HaveRead = model.HaveRead;
        entity.CategoryId = model.CategoryId;
        entity.BorrowedUserId = model.BorrowedUserId;

        return entity;
    }

    public static BookViewModel ToViewModel(Book book)
    {
        return new BookViewModel
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Publisher = book.Publisher,
            PublishDate = book.PublishDate,
            Page = book.Page,
            HaveRead = book.HaveRead,
            IsBorrowed = book.IsBorrowed,
            CategoryId = book.CategoryId,
            BorrowedUserId = book.BorrowedUserId,
            UserId= book.UserId,
            
        };
    }

    public static List<BookViewModel> ToViewModelList(IEnumerable<Book> books)
    {
        return books.Select(ToViewModel).ToList();

    }
}
