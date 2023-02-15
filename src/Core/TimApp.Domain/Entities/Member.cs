using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Common;

namespace TimApp.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<BookTransaction> BookTransactions { get; set; }
    }
}
