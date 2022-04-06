using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Loan
    {
        public int LoanNumber { get; set; }
        public int CopyNumber { get; set; }
        public int MemberNumber { get; set; }
        public DateTime? DateOut { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateReturned { get; set; }
        public int TypeNumber { get; set; }

        public virtual Dvdcopy CopyNumberNavigation { get; set; }
        public virtual Member MemberNumberNavigation { get; set; }
        public virtual LoanType TypeNumberNavigation { get; set; }
    }
}
