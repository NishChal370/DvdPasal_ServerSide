using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class MembershipCategory
    {
        public MembershipCategory()
        {
            Members = new HashSet<Member>();
        }

        public int McategoryNumber { get; set; }
        public string Description { get; set; }
        public int TotalLoans { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
