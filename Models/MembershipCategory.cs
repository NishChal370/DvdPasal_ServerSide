using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("MembershipCategory")]
    public partial class MembershipCategory
    {
        public MembershipCategory()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public int McategoryNumber { get; set; }
        

        public string Description { get; set; }
        public int TotalLoans { get; set; }

        [InverseProperty("CategoryNumberNavigation")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
