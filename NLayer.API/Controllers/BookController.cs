using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly IBookService _service;

        public BookController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _service = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await _service.GetAllAsync();
            var booksDTO = _mapper.Map<List<BookDTO>>(books.ToList());
            return CreateActionResult(CustomResponseDTO<List<BookDTO>>.Success(200, booksDTO));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            var bookDTO = _mapper.Map<BookDTO>(book);
            return CreateActionResult(CustomResponseDTO<BookDTO>.Success(200, bookDTO));

        }

        [HttpGet("borrowed")]
        public async Task<IActionResult> GetBorrowedBooks()
        {
            var borrowedBooks = await _service.GetBorrowedBooksAsync();
            var borrowedBooksDTO = _mapper.Map<List<BorrowedBookDTO>>(borrowedBooks);

            return CreateActionResult(CustomResponseDTO<List<BorrowedBookDTO>>.Success(200,borrowedBooksDTO));
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedBooks()
        {
            var finishedBooks = await _service.GetFinishedBooksAsync();
            var finishedBooksDTO = _mapper.Map<List<FinishedBookDTO>>(finishedBooks);

            return CreateActionResult(CustomResponseDTO<List<FinishedBookDTO>>.Success(200, finishedBooksDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BookPostDTO bookDTO)
        {
            var book = await _service.AddAsync(_mapper.Map<Book>(bookDTO));

            var _bookDTO = _mapper.Map<BookDTO>(book);

            return CreateActionResult(CustomResponseDTO<BookDTO>.Success(201, _bookDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookDTO bookDTO)
        {
            await _service.UpdateAsync(_mapper.Map<Book>(bookDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(book);

            var bookDTO = _mapper.Map<BookDTO>(book);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }
    }
}
