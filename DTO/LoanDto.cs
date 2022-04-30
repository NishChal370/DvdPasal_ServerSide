using DvD_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class LoanDto
    {
        [Required(ErrorMessage = "DvD copy id is required.")]
        public int CopyNumber { get; set; }
        [Required(ErrorMessage = "Member id is required")]
        public int MemberNumber { get; set; }
        [Required(ErrorMessage = "Loan type is required")]
        public LoanType LoanType{ get; set; }
        [Required(ErrorMessage = "Date Out is required")]
        public DateTime DateOut{ get; set; }

    }
}
