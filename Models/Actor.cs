using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("Actor")]
    public partial class Actor
    {
        public Actor()
        {
            Dvdnumbers = new HashSet<Dvdtitle>();
        }

        [Key]
        public int ActorNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string ActorName { get; set; }

        [Required]
        [StringLength(50)]
        public string ActorLastName { get; set; }
        
        public string ProfileUrl { get; set; }

        [ForeignKey("ActorNumber")]
        [InverseProperty("ActorNumbers")]
        public virtual ICollection<Dvdtitle> Dvdnumbers { get; set; }
    }
}
