using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [StringLength(75)]
        
        public string ProducerName { get; set; }

        [InverseProperty("ProducerNumberNavigation")]
        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
