using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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

        [JsonIgnore]
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
        [JsonPropertyName("membershipCategory")]
        public virtual MembershipCategory CategoryNumberNavigation { get; set; }

        [JsonIgnore]
        [InverseProperty("MemberNumberNavigation")]
        public virtual ICollection<Loan> Loans { get; set; }

        public bool IsOldEnough() {
            return DateOfBirth.AddYears(18) <= DateTime.Now;
        }
    }
}
