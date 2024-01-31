using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Mapping;


namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CustomController
    {
        
        private readonly IBookService _service;
        private readonly EntityMapper _entityMapper;

        public BookController(IBookService bookService, EntityMapper entityMapper)
        {
            
            _service = bookService;
            _entityMapper = entityMapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await _service.GetAllAsync();
            var booksDTO = books.Select(book => _entityMapper.BookMapEntityToModel(book)).ToList();
            return CreateActionResult(CustomResponseModel<List<BookModel>>.Success(200, booksDTO));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            var bookDTO = _entityMapper.BookMapEntityToModel(book); // entitytomodel
            return CreateActionResult(CustomResponseModel<BookModel>.Success(200, bookDTO));

        }

        [HttpGet("borrowed")]
        public async Task<IActionResult> GetBorrowedBooks()
        {
            var books = await _service.GetBorrowedBooksAsync();
            var booksModel = books.Select(book => _entityMapper.BookMapEntityToModel(book)).ToList();

            return CreateActionResult(CustomResponseModel<List<BookModel>>.Success(200,booksModel));
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedBooks()
        {
            var books = await _service.GetFinishedBooksAsync();
            var booksModel = books.Select(book => _entityMapper.BookMapEntityToModel(book)).ToList();

            return CreateActionResult(CustomResponseModel<List<BookModel>>.Success(200, booksModel));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BookPostModel bookModel)
        {
            var bookEntity = _entityMapper.BookMapPostModelToEntity(bookModel); //model to entity

            var book = await _service.AddAsync(bookEntity);

            var _bookModel = _entityMapper.BookMapEntityToModel(book);

            return CreateActionResult(CustomResponseModel<BookModel>.Success(200, _bookModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookModel bookModel)
        {
            var bookEntity = await _service.GetByIdAsync(bookModel.Id);
            
            await _service.UpdateAsync(_entityMapper.BookMapPutModelToEntity(bookModel,bookEntity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(book);

         

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }
    }
}
