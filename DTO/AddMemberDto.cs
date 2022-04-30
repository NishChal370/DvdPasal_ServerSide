using DvD_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class AddMemberDto
    {
        [Required(ErrorMessage = "Member first name is required")]
        public string FristName { get; set; }
        [Required(ErrorMessage = "Member last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Member address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Membership category is required")]
        public MembershipCategory MembershipCategory { get; set; }

        [Required(ErrorMessage = "Member date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Member profile image is required is required")]

        public string ProfileImage { get; set; }
    }
}
