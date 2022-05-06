
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Remember me is required.")]
        public bool RememberMe { get; set; }
    }
}