using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class LoanTypeDto
    {
        [Required(ErrorMessage = "Please add loan type name.")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Please add duration for loan type.")]
        public int Duration { get; set; }
    }
}
