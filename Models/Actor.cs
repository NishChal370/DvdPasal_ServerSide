using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("Actor")]
    public partial class Actor
    {
        public Actor()
        {
            DvdNumbers = new HashSet<Dvdtitle>();
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
        [JsonIgnore]
        public virtual ICollection<Dvdtitle> DvdNumbers { get; set; }
    }
}
