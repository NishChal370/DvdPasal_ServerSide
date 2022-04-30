using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class CopyDto
    {
        [Required(ErrorMessage = "DvD Id is required to add copy.")]
        public int DvdId { get; set; }
        [Required(ErrorMessage = "Date purchased is required to add copy.")]
        public DateTime DatePurchased { get; set; }
    }
}
