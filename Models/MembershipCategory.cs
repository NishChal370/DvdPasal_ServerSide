using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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

        [Required(ErrorMessage = "Total loans is required.")]
        [Range(1, 30, ErrorMessage = "Total loans should be more than one and less than 30")]
        public int TotalLoans { get; set; }

        [InverseProperty("CategoryNumberNavigation")]
        [JsonIgnore]
        public virtual ICollection<Member> Members { get; set; }
    }
}
