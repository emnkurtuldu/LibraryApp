using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Common;

namespace TimApp.Applicaiton.Interfaces.Repository
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity 
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllPagedAsync(int page);
        Task<T> GetByIdAsync(Guid id);

    }
}
