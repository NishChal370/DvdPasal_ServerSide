using System.ComponentModel.DataAnnotations;

namespace DvD_Api.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Username cannot be more than 30 characters.")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
