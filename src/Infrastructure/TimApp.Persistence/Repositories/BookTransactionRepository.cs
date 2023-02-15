using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Applicaiton.Interfaces.Repository;
using TimApp.Domain.Entities;
using TimApp.Persistence.Context;

namespace TimApp.Persistence.Repositories
{
    public class BookTransactionRepository : GenericRepository<BookTransaction>, IBookTransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookTransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookTransaction> FindBookTransactionFromIdAsync(Guid id)
        {
            return await _dbContext.BookTransactions.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<BookTransaction>> GetUpcomingDueDateOrLatedDelivededBookTransactionAsync(int page)
        {
            return await _dbContext.BookTransactions
                .Where(s => (s.DeliveredDate == null && s.DueDate.Date >= DateTime.Now.Date.AddDays(-2)) || (s.DeliveredDate >= s.DueDate))
                .Skip(page-1).Take(GlobalParams.pageSize)
                .ToListAsync();
            
        }
    }
}
