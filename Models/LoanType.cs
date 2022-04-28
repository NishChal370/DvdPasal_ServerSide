using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("LoanType")]
    public partial class LoanType
    {
        public LoanType()
        {
            Loans = new HashSet<Loan>();
        }

        [Key]
        public int LoanTypeNumber { get; set; }
        [Required]
        
        
        public string LoanTypeName { get; set; }
        public int? Duration { get; set; }

        [InverseProperty("TypeNumberNavigation")]
        [JsonIgnore]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
