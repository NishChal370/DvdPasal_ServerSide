using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Dvdcopy
    {
        public Dvdcopy()
        {
            Loans = new HashSet<Loan>();
        }

        public int CopyNumber { get; set; }
        public int Dvdnumber { get; set; }
        public DateTime DatePurchased { get; set; }

        public virtual Dvdtitle DvdnumberNavigation { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
