using DvD_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class LoanDto
    {
        [Required]
        public int CopyNumber { get; set; }
        [Required]
        public int MemberNumber { get; set; }
        [Required]
        public LoanType LoanType{ get; set; }
        [Required]
        public DateTime DateOut{ get; set; }

    }
}
