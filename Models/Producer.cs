using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("Producer")]
    public partial class Producer
    {
        public Producer()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        [Key]
        public int ProducerNumber { get; set; }
        [Required(ErrorMessage = "Producer name is required")]
        [StringLength(75, ErrorMessage = "Producer name must be shorter than 75 characters.")]
        
        public string ProducerName { get; set; }

        [InverseProperty("ProducerNumberNavigation")]
        [JsonIgnore]
        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
