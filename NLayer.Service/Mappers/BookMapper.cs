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
            CategoryId = model.CategoryId,
            OwnerId = model.OwnerId
           
        };
    }

    public static Book ToEntity(BookUpdateModel model, Book entity)
    {
        entity.Name = model.Name ?? entity.Name;
        entity.Author = model.Author ?? entity.Author;
        entity.Publisher = model.Publisher ?? entity.Publisher;
        entity.Page = model.Page;
        entity.HaveRead = model.HaveRead;
        entity.CategoryId = model.CategoryId;
        

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
            IsBorrowed = book.IsBorrowed ?? false,
            CategoryId = book.CategoryId,
            BorrowerId = book.BorrowerId ?? 0 ,
            OwnerId= book.OwnerId,
            
        };
    }

    public static List<BookViewModel> ToViewModelList(IEnumerable<Book> books)
    {
        return books.Select(ToViewModel).ToList();

    }
}
