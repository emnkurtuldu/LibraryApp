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
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member> FindMemberFromIdAsync(Guid id)
        {
            return await _dbContext.Members.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
