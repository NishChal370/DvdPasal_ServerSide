using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("Member")]
    public partial class Member
    {
        public Member()
        {
            Loans = new HashSet<Loan>();
        }

        [Key]
        public int MemberNumber { get; set; }
        public int CategoryNumber { get; set; }
        [Required]
        
        
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        
        
        public string ProfileImage64 { get; set; }

        [ForeignKey("CategoryNumber")]
        [InverseProperty("Members")]
        public virtual MembershipCategory CategoryNumberNavigation { get; set; }
        [InverseProperty("MemberNumberNavigation")]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
