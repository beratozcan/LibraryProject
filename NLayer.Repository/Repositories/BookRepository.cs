using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Service.Exceptions;


namespace NLayer.Repository.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        IBorrowedBooksLoggerRepository _loggerRepository;
        

        public BookRepository(AppDbContext context, IBorrowedBooksLoggerRepository loggerRepository) : base(context) 
        {
            _loggerRepository = loggerRepository;
            
        }

        public async Task BorrowBookAsync(int bookId, int borrowerId)
        {
            var bookEntity = await _context.Books.FindAsync(bookId);

            if (bookEntity!.BookStatusId != GlobalConstants.didBorrow && bookEntity.BookStatusId == GlobalConstants.canBorrow)
            {
                bookEntity.BorrowerId = borrowerId;
                bookEntity.BookStatusId = 3;
                await _context.SaveChangesAsync();
                await _loggerRepository.LogBorrowedBookHistoryAsync(bookId, borrowerId);
            }
            else
            {
                throw new Exception("Kitap odunc almaya musait degil");
            }
        }
        

        public async Task<ICollection<Book>> GetBooksByStatus(int status)
        {
            var books = await _context.Books.Where(b=> b.BookStatusId == status).ToListAsync();
            return books;
        }
        
        public async Task GiveBookToOwnerAsync(int bookId)
        {
            var bookEntity = await _context.Books.FindAsync(bookId);

            if(bookEntity !=null && bookEntity.BookStatusId == GlobalConstants.didBorrow )
            {
                bookEntity.Borrower = null;
                bookEntity.BookStatusId = GlobalConstants.canBorrow;
            }

            await _loggerRepository.LogGiveBackBookHistoryAsync(bookId);
        }

        public async Task AddBookToCategoryAsync(int bookId, int categoryId)
        {
            var entity = new BookCategory { BookId = bookId, CategoryId = categoryId };

            await _context.BookCategories.AddAsync(entity);

            await _context.SaveChangesAsync();

        }

        public override async Task<ICollection<Book>> GetAllAsync()
        {
            return await _context.Books.Select(b => new Book
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
                }).ToList(),

            }).ToListAsync();
        }

        public override async Task<Book> GetByIdAsync(int id)
        {
            var entity = await _context.Books
                .Where(b => b.Id == id)
                .Select(b => new Book
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    PublishDate = b.PublishDate,
                    Page = b.Page,
                    GenreId = b.GenreId,
                    BorrowerId = b.BorrowerId,
                    BookStatusId = b.BookStatusId,
                    OwnerId = b.OwnerId,
                    
                    BookCategories = b.BookCategories.Select(bc => new BookCategory
                    {
                        Category = new Category
                        {
                            Id = bc.Category.Id,
                            Name = bc.Category.Name
                        }
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            if(entity != null)
            {
                return entity;
            }
            else
            {
                throw new NotFoundException($"Book not found with ID: {id}");
            }
        }

        public bool DoesUserHaveBook(int userId, int bookId)
        {
            var bookEntity = _context.Books.FirstOrDefault(b =>  b.Id == bookId);

            if (bookEntity == null)
            {
                return false;
            }

            var userEntity = _context.Users.Include(u => u.OwnedBooks)
                                            .FirstOrDefault(u => u.Id == userId);
            if (userEntity == null)
            {
                return false;
            }

            bool DoesUserHaveBook = userEntity.OwnedBooks.Any(book => book.Id == bookId);

            return DoesUserHaveBook;
        }
    }  
    
}

