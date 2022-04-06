using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Member
    {
        public Member()
        {
            Loans = new HashSet<Loan>();
        }

        public int MemberNumber { get; set; }
        public int CategoryNumber { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual MembershipCategory CategoryNumberNavigation { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
