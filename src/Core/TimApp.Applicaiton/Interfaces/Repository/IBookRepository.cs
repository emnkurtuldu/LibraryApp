using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Applicaiton.Dto;
using TimApp.Domain.Entities;

namespace TimApp.Applicaiton.Interfaces.Repository
{
    public interface IBookRepository : IGenericRepositoryAsync<Book>
    {
        Task<ICollection<Book>> GetAvailableBooksAsync();
        Task<ICollection<Book>> GetAvailablePagedBooksAsync(int page);
        Task<ICollection<Book>> GetAvailablePagedBooksAsync(BookDto bookDto, int page);
        Task<Book> FindBookFromISBNAsync(string isbn);
        Task<Book> FindBookFromIdAsync(Guid id);
    }
}
