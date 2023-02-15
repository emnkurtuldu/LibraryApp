using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Common;

namespace TimApp.Domain.Entities
{
    public class BookTransaction : BaseEntity
    {
        public string RelatedMemberId { get; set; }
        public string RelatedISBN { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public double DelayPenaltyFee { get; set; }
        public virtual Book Book { get; set; }
        public virtual Member Member { get; set; }


    }
}
