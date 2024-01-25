

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class BookService : Service<Book>, IBookService
    {

        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> repository,IUnitOfWork unitOfWork,IMapper mapper, IBookRepository bookRepository)
            :base(repository,unitOfWork)
        {
            _mapper = mapper;
            _repository = bookRepository;
            
        }
        
        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _repository.GetBorrowedBooksAsync();
        }
    }
}
