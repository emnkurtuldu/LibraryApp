using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimApp.Domain.Common;

namespace TimApp.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public virtual ICollection<BookTransaction> BookTransactions { get; set; }

    }
}
