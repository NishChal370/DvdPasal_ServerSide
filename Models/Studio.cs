using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("Studio")]
    public partial class Studio
    {
        public Studio()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        [Key]
        public int StudioNumber { get; set; }
        [Required]
        
        
        public string StudioName { get; set; }

        [InverseProperty("StudioNumberNavigation")]
        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
