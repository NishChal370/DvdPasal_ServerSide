using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.Models
{
    public class RopeyUserDto: IdentityUser
    {

        [Required]
        [MaxLength(30, ErrorMessage = "First name must be 30 characters or less.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }


        [Required]
        [MaxLength(30, ErrorMessage = "First name must be 30 characters or less.")]
        public string LastName { get; set; }


    }
}
