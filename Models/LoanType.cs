using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class LoanType
    {
        public LoanType()
        {
            Loans = new HashSet<Loan>();
        }

        public int LoanTypeNumber { get; set; }
        public string LoanType1 { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
