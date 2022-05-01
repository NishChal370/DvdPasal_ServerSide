using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class PasswordChangeDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
