using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CustomController
    {

        private readonly IBookService _service;
        private readonly IUserService _userService;
        public BookController(IBookService bookService, IUserService userService)
        {

            _service = bookService;
            _userService = userService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
  
                var books = await _service.GetAllAsync();
                var booksModel = BookMapper.ToViewWithCategoriesModelList(books);
                return CreateActionResult(CustomResponseModel<List<BookViewWithCategoriesModel>>.Success(200, booksModel));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var book = await _service.GetByIdAsync(id);
                var bookModel = BookMapper.ToViewWithCategoriesModel(book);
                return CreateActionResult(CustomResponseModel<BookViewWithCategoriesModel>.Success(200, bookModel));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
             

        }

        [HttpGet("GetBookByStatus")]

        public async Task<IActionResult> GetBookByStatus(int statusId,int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var books = await _service.GetBooksByStatus(statusId);

                var booksModel = BookMapper.ToViewModelList(books);

                return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200, booksModel));

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }       
        }


        [HttpPost]
        public async Task<IActionResult> Create(BookCreateModel bookModel, int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var bookEntity = BookMapper.ToEntity(bookModel);

                var book = await _service.AddAsync(bookEntity);

                var _bookModel = BookMapper.ToViewModel(book);

                return CreateActionResult(CustomResponseModel<BookViewModel>.Success(200, _bookModel));

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateModel bookModel, int updatedBookId)
        {
            var doesUserHaveBook = _service.DoesUserHaveBook(id, updatedBookId);
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin && doesUserHaveBook)
            {
                var bookEntity = await _service.GetByIdAsync(updatedBookId);

                await _service.UpdateAsync(BookMapper.ToEntity(bookModel, bookEntity));

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else if(!doesUserHaveBook)
            {
                throw new Exception("Kullanici bu kitaba sahip degil");

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id, int deletedBookId)
        {
            var didUserLogin = _userService.DidUserLogin(id);
            var doesUserHaveBook = _service.DoesUserHaveBook(id, deletedBookId);

            if (didUserLogin && doesUserHaveBook)
            {
                var entity = await _service.GetByIdAsync(deletedBookId);

                await _service.RemoveAsync(entity);

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else if(!doesUserHaveBook)
            {
                throw new Exception("Kullanici bu kitaba sahip degil");
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }

            
        }

        [HttpPut("BorrowBook")]

        public async Task<IActionResult> BorrowBookAsync(int bookId, int borrowerId,int userId)
        {
            var didUserLogin = _userService.DidUserLogin(userId);

            if(didUserLogin)
            {
                await _service.BorrowBookAsync(bookId, borrowerId);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }

           

        }

        [HttpPut("GiveBookToOwner")]

        public async Task<IActionResult> GiveBookToOwnerAsync(int bookId, int userId)
        {
            var didUserLogin = _userService.DidUserLogin(userId);

            if(didUserLogin)
            {
                await _service.GiveBookToOwnerAsync(bookId);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }

            
        }

        [HttpPut("AddBookToCategory")]

        public async Task<IActionResult> AddBookToCategory(int bookId, int categoryId,int userId)
        {
            var doesUserHaveBook = _service.DoesUserHaveBook(userId,bookId);
            var didUserLogin = _userService.DidUserLogin(userId);

            if (didUserLogin && doesUserHaveBook)
            {
                await _service.AddBookToCategoryAsync(bookId, categoryId);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else if (!doesUserHaveBook)
            {
                throw new Exception("Kullanici bu kitaba sahip degil");
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
            
        }
    }
    
}
