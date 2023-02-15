using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TimApp.Applicaiton.Dto;
using TimApp.Applicaiton.Interfaces.Repository;
using TimApp.Domain.Entities;
using TimApp.Persistence;

namespace TimApp.WebUI.Controllers
{
    public class BookTransactionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookTransactionRepository _bookTransactionRepository;

        public BookTransactionController(IHttpContextAccessor httpContextAccessor, IBookTransactionRepository bookTransactionRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookTransactionRepository = bookTransactionRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var bookTansactions = await _bookTransactionRepository.GetAllPagedAsync(page);
            var bookTransactionDtos = ConvertEntityToDto(bookTansactions);
            return View(bookTransactionDtos);
        }

        public async Task<IActionResult> Daily(int page = 1)
        {
            var bookTansactions = await _bookTransactionRepository.GetUpcomingDueDateOrLatedDelivededBookTransactionAsync(page);
            var bookTransactionDtos = ConvertEntityToDto(bookTansactions);
            return View(bookTransactionDtos);
        }

        [HttpPost]
        public async Task<IActionResult> RentBook(BookTransactionDto bookTransactionDto)
        {
            var bookTransaction = new BookTransaction()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                RelatedMemberId = bookTransactionDto.RelatedMemberId,
                RelatedISBN = bookTransactionDto.RelatedISBN,
                DueDate = bookTransactionDto.DueDate
            };
            await _bookTransactionRepository.CreateAsync(bookTransaction);
            return RedirectToAction("Index","Book");
        }


        [HttpPost]
        public async Task<IActionResult> ReceiveBook(BookTransactionDto bookTransactionDto)
        {
            var bookTransaction = await _bookTransactionRepository.FindBookTransactionFromIdAsync(bookTransactionDto.Id);
            bookTransaction.DeliveredDate = bookTransactionDto.DeliveredDate.Value;
            bookTransaction.DelayPenaltyFee = GlobalUtils.CalculateDelayPenaltyFee((bookTransactionDto.DeliveredDate.Value.Date - bookTransactionDto.DueDate.Date).TotalDays);
            await _bookTransactionRepository.UpdateAsync(bookTransaction);

            return Redirect(_httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString());
        }



        private ICollection<BookTransactionDto> ConvertEntityToDto(ICollection<BookTransaction> bookTransactions)
        {
            ICollection<BookTransactionDto> bookTansactionsDto = bookTransactions.Select(s => new BookTransactionDto()
            {
                Id = s.Id,
                DeliveredDate = s.DeliveredDate,
                DueDate = s.DueDate,
                RelatedISBN = s.RelatedISBN,
                RelatedMemberId = s.RelatedMemberId,
                DelayPenaltyFee = s.DelayPenaltyFee,
            }).ToList();
            return bookTansactionsDto;
        }
    }
}
