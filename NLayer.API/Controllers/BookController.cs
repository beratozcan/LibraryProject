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
        public BookController(IBookService bookService)
        {
            
            _service = bookService;
            
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await _service.GetSoftRemovedAllAsync();
            var booksDTO = BookMapper.ToViewModelList(books);
            return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200, booksDTO));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            var bookDTO = BookMapper.ToViewModel(book);
            return CreateActionResult(CustomResponseModel<BookViewModel>.Success(200, bookDTO));

        }

        [HttpGet("borrowed")]
        public async Task<IActionResult> GetBorrowedBooksAsync()
        {
            var books = await _service.GetBorrowedBooksAsync();
            var booksModel = BookMapper.ToViewModelList(books);

            return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200,booksModel));
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedBooks()
        {
            var books = await _service.GetFinishedBooksAsync();
            var booksModel = BookMapper.ToViewModelList(books);

            return CreateActionResult(CustomResponseModel<List<BookViewModel>>.Success(200, booksModel));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BookCreateModel bookModel)
        {
            var bookEntity = BookMapper.ToEntity(bookModel); //model to entity

            var book = await _service.AddAsync(bookEntity);

            var _bookModel = BookMapper.ToViewModel(book);

            return CreateActionResult(CustomResponseModel<BookViewModel>.Success(200, _bookModel));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,BookUpdateModel bookModel)
        {

            var bookEntity = await _service.GetByIdAsync(id);
            
            await _service.UpdateAsync(BookMapper.ToEntity(bookModel, bookEntity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.SoftDeleteAsync(id);

           
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpPut("BorrowBook")]

        public async Task<IActionResult> BorrowBookAsync(int bookId, int borrowerId)
        {
            await _service.BorrowBookAsync(bookId, borrowerId);
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

        }

        [HttpPut("GiveBookToOwner")]

        public async Task<IActionResult> GiveBookToOwnerAsync(int bookId)
        {

            await _service.GiveBookToOwnerAsync(bookId);
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        
    }
}
