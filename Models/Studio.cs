using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
        [Required(ErrorMessage = "Studio name is required.")]
        
        
        public string StudioName { get; set; }

        [InverseProperty("StudioNumberNavigation")]
        [JsonIgnore]
        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
