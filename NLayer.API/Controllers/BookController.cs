using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Exceptions;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : CustomController
    {


        private readonly IBookService _service;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;
        public BookController(IBookService bookService, IUserService userService, IUserTokenService userTokenService)
        {
            _service = bookService;
            _userService = userService;
            _userTokenService = userTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accesToken = _userTokenService.TakeAccessToken(HttpContext);
                try
                {
                    var books = await _service.GetAllAsync(accesToken);
                    var booksModel = BookMapper.ToViewWithCategoriesModelList(books);
                    return CreateActionResult(CustomResponseModel<List<BookViewWithCategoriesModel>>.Success(200, booksModel));
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var book = await _service.GetByIdAsync(id, accessToken);
                    var bookModel = BookMapper.ToViewWithCategoriesModel(book);
                    return CreateActionResult(CustomResponseModel<BookViewWithCategoriesModel>.Success(200, bookModel));
                }
                catch (NotFoundException ex)
                {

                    return NotFound($"Book not found with ID: {id}");
                }
                catch(UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpGet("GetBookByStatus")]

        public async Task<IActionResult> GetBookByStatus(int statusId)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var books = await _service.GetBooksByStatus(statusId, accessToken);

                    var booksModel = BookMapper.ToViewModelList(books);

                    return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200, booksModel));
                }
                catch (NotFoundException ex)
                {
                    return NotFound("Book not found");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");  
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateModel bookModel)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                try
                { 
                    var bookEntity = BookMapper.ToEntity(bookModel);

                    bookEntity.OwnerId = _userService.GetAuthenticatedUserId(accessToken);

                    var book = await _service.AddAsync(bookEntity);

                    var _bookModel = BookMapper.ToViewModel(book);

                    return CreateActionResult(CustomResponseModel<BookViewModel>.Success(200, _bookModel));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated"); 
            
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> Update(BookUpdateModel bookModel, int bookId)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var bookEntity = await _service.GetByIdAsync(bookId, accessToken);

                    await _service.UpdateAsync(BookMapper.ToEntity(bookModel, bookEntity));

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch(UnauthorizedAccessException ex) 
                {
                    return Unauthorized(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    return NotFound($"Book not found with ID: {bookId}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
            
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int deletedBookId)
        {

            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var entity = await _service.GetByIdAsync(deletedBookId, accessToken);

                    await _service.RemoveAsync(entity);

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpPut("AddBookToCategory")]

        public async Task<IActionResult> AddBookToCategory(int bookId, int categoryId)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                await _service.AddBookToCategoryAsync(bookId, categoryId, accessToken);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpPut("RemoveBookFromCategory")]

        public async Task<IActionResult> RemoveBookFromCategory(int bookId, int categoryId)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    await _service.RemoveBookFromCategoryAsync(bookId, categoryId, accessToken);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User authenticate degil");
        }
    }
    
}
