using DvD_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class AddMemberDto
    {
        [Required]
        public string FristName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public MembershipCategory MembershipCategory { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]

        public string ProfileImage { get; set; }
    }
}
