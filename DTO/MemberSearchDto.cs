using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class MemberSearchDto
    {
        [Required]
        public string SearchTerm { get; set; }

        public bool IsLastName { get; set; }
        
    }
}
