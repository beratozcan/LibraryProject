using Microsoft.EntityFrameworkCore;
using NLayer.Core;

using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Repository.Exceptions;
using NLayer.Service.Exceptions;
using System.Net;


namespace NLayer.Repository.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        IBorrowedBooksLoggerRepository _loggerRepository;
        IUserTokenRepository _userTokenRepository;

        public BookRepository(AppDbContext context, IBorrowedBooksLoggerRepository loggerRepository, IUserTokenRepository userTokenRepository) : base(context) 
        {
            _loggerRepository = loggerRepository;
            _userTokenRepository = userTokenRepository;
            
        }
        public async Task BorrowBookAsync(int bookId, string token)
        {
           
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            var bookEntity = await _context.Books.FirstOrDefaultAsync(b=> b.Id == bookId)
                                                                     ?? throw new NotFoundException("Boyle bir kitap yok");

            if (bookEntity.BookStatusId != GlobalConstants.canBorrow)
            {
                throw new BusinessExceptions("Book is not available for borrowing.");
            }

            if (bookEntity.OwnerId == userTokenEntity.UserId)
            {
                throw new BusinessExceptions("Kitap zaten bu kullanıcıya ait");
            }
            
            bookEntity.BorrowerId = userTokenEntity.UserId;
            bookEntity.BookStatusId = GlobalConstants.didBorrow;

            await _context.SaveChangesAsync();
            await _loggerRepository.LogBorrowedBookHistoryAsync(bookId, userTokenEntity.UserId);
        }
        public async Task<ICollection<Book>> GetBooksByStatus(int status, string token)
        { 
            var books = await _context.Books
                .Where(b => b.BookStatusId == status  )
                .ToListAsync();

            if (books == null)
            {
                throw new NotFoundException("Kitap bulunamadi");
            }

            return books;
        }

        public async Task GiveBookToOwnerAsync(int bookId, string token)
        {

            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            var bookEntity = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId)
                                                                     ?? throw new NotFoundException("Boyle bir kitap yok");


            if (bookEntity.BookStatusId == GlobalConstants.didBorrow && bookEntity.BorrowerId == userTokenEntity.UserId)
            {
                bookEntity.BorrowerId = null;
                bookEntity.BookStatusId = GlobalConstants.canBorrow;
                await _context.SaveChangesAsync();
                await _loggerRepository.LogGiveBackBookHistoryAsync(bookId);
            }
            else
            {
                throw new BusinessExceptions("Kitap zaten odunc alinmamis");
            }
            
        }
        public async Task AddBookToCategoryAsync(int bookId, int categoryId, string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);
            var categoryEntity = _context.Categories.FirstOrDefault(c => c.Id == categoryId && c.UserId == userTokenEntity.UserId)
                                                                    ?? throw new UnauthorizedAccessException("Kullanici boyle bir categorye sahip degil");

            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == bookId && b.OwnerId == userTokenEntity.UserId)
                                                             ??throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            var entity = new BookCategory { BookId = bookId, CategoryId = categoryId };
            await _context.BookCategories.AddAsync(entity);

            await _context.SaveChangesAsync();
        }
        public override async Task<ICollection<Book>> GetAllAsync(string token)
        {
            var userTokenEntity = _context.UserTokens.FirstOrDefault(u => u.Token == token);

            var books = await _context.Books
        .Where(b => b.OwnerId == userTokenEntity.UserId || b.BorrowerId == userTokenEntity.UserId)
        .Select(b => new Book
        {
            Id = b.Id,
            Name = b.Name,
            Author = b.Author,
            Publisher = b.Publisher,
            PublishDate = b.PublishDate,
            Page = b.Page,
            GenreId = b.GenreId,
            OwnerId = b.OwnerId,
            BorrowerId = b.BorrowerId,
            BookStatusId = b.BookStatusId,
            BookCategories = b.BookCategories.Select(bc => new BookCategory
            {
                Category = new Category
                {
                    Id = bc.Category.Id,
                    Name = bc.Category.Name
                }
            }).ToList()
        })
        .ToListAsync();

            return books;

        }
        public override async Task<Book> GetByIdAsync(int id, string token)
        {
            var userTokenEntity = await _context.UserTokens.FirstOrDefaultAsync(u => u.Token == token);

            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == id && b.OwnerId == userTokenEntity.UserId)
                                                             ?? throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            var _bookEntity = await _context.Books
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Id == id);


            return _bookEntity;
        }
        public override void Remove(Book entity)
        {
            var doesUserHaveBook = _context.Books.FirstOrDefault(b => b.OwnerId == entity.OwnerId)
                ?? throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            entity.IsDeleted = true;
            _context.SaveChanges();
        }

        public async Task RemoveBorrowedBookAsync(int bookId, string token)
        {
            var userTokenEntity = await _context.UserTokens.FirstOrDefaultAsync(u => u.Token == token);

            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == bookId && b.OwnerId == userTokenEntity.UserId)
                                                             ?? throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            if (bookEntity.BookStatusId != GlobalConstants.canBorrow)
            {
                throw new BusinessExceptions("Kitap zaten odunc verilebilir degil");
            }

            bookEntity.BookStatusId = GlobalConstants.didBorrow;

            await _context.SaveChangesAsync();
        }

        public async Task AddBookToBorrowedBooksAsync(int bookId, string token)
        {
            var userTokenEntity = await _context.UserTokens.FirstOrDefaultAsync(u => u.Token == token);

            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == bookId && b.OwnerId == userTokenEntity.UserId)
                                                             ?? throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            bookEntity.BookStatusId = GlobalConstants.canBorrow;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookFromCategoryAsync(int bookId, int categoryId, string token)
        {
            var userTokenEntity = await _context.UserTokens.FirstOrDefaultAsync(u => u.Token == token);

            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == bookId && b.OwnerId == userTokenEntity.UserId)
                                                             ?? throw new UnauthorizedAccessException("Kullanici boyle bir kitaba sahip degil");

            var categoryEntity = _context.Categories.FirstOrDefault(c => c.UserId == userTokenEntity.UserId && c.Id == categoryId)
                                                    ?? throw new UnauthorizedAccessException("Kullanici boyle bir categorye sahip degil");

            var bookCategoryEntity = _context.BookCategories.FirstOrDefault(bc => bc.CategoryId == categoryId && bc.BookId == bookId);

            if (bookCategoryEntity != null)
            {
                _context.BookCategories.Remove(bookCategoryEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Kitap belirtilen kategoride bulunamadi");
            }
        }
    }  
}

