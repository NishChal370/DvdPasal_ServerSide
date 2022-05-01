using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class UserPasswordDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
