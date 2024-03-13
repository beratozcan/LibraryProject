using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Repository.Exceptions;
using NLayer.Service.Exceptions;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowBookController : CustomController
    {
        private readonly IBookService _bookService;
        private readonly IUserTokenService _userTokenService;
        public BorrowBookController(IBookService bookService,  IUserTokenService userTokenService)
        {
            _bookService = bookService;
            _userTokenService = userTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int bookStatus = GlobalConstants.canBorrow;

            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticated)
            {
                try
                {
                    var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                    var books = _bookService.GetBooksByStatus(bookStatus, accessToken);

                    var booksModel = BookMapper.ToViewModelList(await books);

                    return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200, booksModel));
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

        [HttpPut("BorrowABook")]
        public async Task<IActionResult> BorrowBookAsync(int bookId)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {
                try
                {
                    var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                    await _bookService.BorrowBookAsync(bookId, accessToken);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch(BusinessExceptions ex)
                {
                    return BadRequest(ex.Message);
                }
                catch(NotFoundException ex)
                {
                    return NotFound(ex.Message) ;
                }

            }

            return Unauthorized("User is not authenticated");
        }

        [HttpPut("GiveBookToOwner")]
        public async Task<IActionResult> GiveBookToOwnerAsync(int bookId)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {
                try
                {
                    var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                    await _bookService.GiveBookToOwnerAsync(bookId, accessToken);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch(BusinessExceptions ex)
                {
                    return BadRequest(ex.Message);
                }
                catch(NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }

            }

            return Unauthorized("User is not authenticated");
        }

        [HttpPost]
        public async Task<IActionResult> Create(int bookId)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticated)
            {
                var accesToken = _userTokenService.TakeAccessToken(HttpContext);
                
                    try
                    {
                        await _bookService.AddBookToBorrowedBooksAsync(bookId, accesToken);
                        return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
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
                    await _bookService.RemoveBorrowedBookAsync(deletedBookId,accessToken);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch (BusinessExceptions ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
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
    }
}
