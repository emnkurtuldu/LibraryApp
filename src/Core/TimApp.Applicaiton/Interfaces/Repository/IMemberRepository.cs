using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Entities;

namespace TimApp.Applicaiton.Interfaces.Repository
{
    public interface IMemberRepository : IGenericRepositoryAsync<Member>
    {
        Task<Member> FindMemberFromIdAsync(Guid id);
    }
}
