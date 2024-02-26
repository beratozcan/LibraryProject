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
            BookStatusId = model.BookStatusId,
            OwnerId = model.OwnerId,
            GenreId = model.GenreId,
            
           
        };
    }

    public static Book ToEntity(BookUpdateModel model, Book entity)
    {
        entity.Name = model.Name ?? entity.Name;
        entity.Author = model.Author ?? entity.Author;
        entity.Publisher = model.Publisher ?? entity.Publisher;
        entity.Page = model.Page;
        entity.BookStatusId = model.BookStatusId;
        entity.GenreId = model.GenreId;
        
        

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
            BookStatusId = book.BookStatusId,
            /*Categories =  book.BookCategories == null ?  []  : book.BookCategories.
            Select(x => CategoryMapper.ToViewModel(x.Category)).ToList() */
            GenreId = book.GenreId ,
            OwnerId= book.OwnerId,
            BorrowerId = book.BorrowerId
            
        };
    }

    public static BookViewWithCategoriesModel ToViewWithCategoriesModel(Book book)
    {
        return new BookViewWithCategoriesModel
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Publisher = book.Publisher,
            PublishDate = book.PublishDate,
            Page = book.Page,
            BookStatusId = book.BookStatusId,
            Categories =  book.BookCategories == null ?  []  : book.BookCategories.
            Select(x => CategoryMapper.ToViewModel(x.Category)).ToList(),
            GenreId = book.GenreId,
            OwnerId = book.OwnerId,
            BorrowerId = book.BorrowerId

        };
    }

    public static List<BookViewModel> ToViewModelList(IEnumerable<Book> books)
    {
        return books.Select(ToViewModel).ToList();

    }

    public static List<BookViewWithCategoriesModel> ToViewWithCategoriesModelList(IEnumerable<Book> books)
    {
        return books.Select(ToViewWithCategoriesModel).ToList();

    }
}
