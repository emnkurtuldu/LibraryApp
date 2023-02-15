using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Applicaiton.Dto;
using TimApp.Applicaiton.Interfaces.Repository;
using TimApp.Domain.Entities;
using TimApp.Persistence.Context;

namespace TimApp.Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> FindBookFromIdAsync(Guid id)
        {
            return await _dbContext.Books.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> FindBookFromISBNAsync(string isbn)
        {
            return await _dbContext.Books.Where(s => s.ISBN == isbn).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Book>> GetAvailableBooksAsync()
        {
           return await _dbContext.Books.Include(u => u.BookTransactions).Where(u => !u.BookTransactions.Any(s=> s.DeliveredDate == null )).ToListAsync();
        }

        public async Task<ICollection<Book>> GetAvailablePagedBooksAsync(int page)
        {
            return await _dbContext.Books.Include(u => u.BookTransactions).Where(u => !u.BookTransactions.Any(s => s.DeliveredDate == null)).Skip(page - 1).Take(GlobalParams.pageSize).ToListAsync();
        }

        public async Task<ICollection<Book>> GetAvailablePagedBooksAsync(BookDto bookDto, int page)
        {
            var books = _dbContext.Books
                .Include(u => u.BookTransactions)
                .Where(u => !u.BookTransactions
                .Any(s => s.DeliveredDate == null))
                .Skip(page - 1)
                .Take(GlobalParams.pageSize);

            if (!string.IsNullOrEmpty(bookDto.ISBN))
                books = books.Where(s => s.ISBN.Contains(bookDto.ISBN));
            if (!string.IsNullOrEmpty(bookDto.Name))
                books = books.Where(s => s.Name.ToLower().Contains(bookDto.Name.ToLower()));

            return await books.ToListAsync();
        }
    }
}
