using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context): base(context) 
        {
                
        } 
        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _context.Set<Book>().Where(b => b.isBorrowed).ToListAsync();
        }
    }
}
