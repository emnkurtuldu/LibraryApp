using Microsoft.AspNetCore.Mvc;
using TimApp.Applicaiton.Dto;
using TimApp.Applicaiton.Interfaces.Repository;
using TimApp.Domain.Entities;
using TimApp.Persistence;

namespace TimApp.WebUI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var books = await _bookRepository.GetAvailablePagedBooksAsync(page);
            ICollection<BookDto> bookDtos = ConvertEntityToDto(books);

            return View(bookDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Index(BookDto bookDto, int page = 1)
        {
            var books = await _bookRepository.GetAvailablePagedBooksAsync(bookDto,page);
            ICollection<BookDto> bookDtos = ConvertEntityToDto(books);

            return View(bookDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                ISBN = bookDto.ISBN,
                Author = bookDto.Author,
                Name = bookDto.Name,
            };
            await _bookRepository.CreateAsync(book);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookDto bookDto)
        {
            var book = await _bookRepository.FindBookFromIdAsync(bookDto.Id);
            book.ISBN = bookDto.ISBN;
            book.Author = bookDto.Author;
            book.Name = bookDto.Name;
            
            await _bookRepository.UpdateAsync(book);
            return RedirectToAction("Index");
        }


        private ICollection<BookDto> ConvertEntityToDto(ICollection<Book> books)
        {
            return books.Select(s => new BookDto()
            {
                Id = s.Id,
                ISBN = s.ISBN,
                Name = s.Name,
                Author = s.Author

            }).ToList();
        }
    }
}
