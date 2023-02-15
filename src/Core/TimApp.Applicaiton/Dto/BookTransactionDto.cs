using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimApp.Applicaiton.Dto
{
    public class BookTransactionDto
    {
        public Guid Id { get; set; }   
        public Guid BookId { get; set; }
        public string RelatedMemberId { get; set; }
        public string RelatedISBN { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public Double DelayPenaltyFee { get; set; }
    }
}
