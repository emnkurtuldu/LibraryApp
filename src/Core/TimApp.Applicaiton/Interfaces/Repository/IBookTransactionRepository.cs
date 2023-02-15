using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Entities;

namespace TimApp.Applicaiton.Interfaces.Repository
{
    public interface IBookTransactionRepository : IGenericRepositoryAsync<BookTransaction>
    {
        Task<ICollection<BookTransaction>> GetUpcomingDueDateOrLatedDelivededBookTransactionAsync(int page);
        Task<BookTransaction> FindBookTransactionFromIdAsync(Guid id);
    }
}
