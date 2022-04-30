using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("DVDTitle")]
    public partial class Dvdtitle
    {
        public Dvdtitle()
        {
            DvDimages = new HashSet<DvDimage>();
            Dvdcopies = new HashSet<Dvdcopy>();
            ActorNumbers = new HashSet<Actor>();
        }

        [Key]
        public int DvdNumber { get; set; }
        

        [StringLength(100, ErrorMessage = "DvD name should be 100 characters or less.")]
        
        public string DvdName { get; set; }

        [JsonIgnore]
        public int ProducerNumber { get; set; }
        [JsonIgnore]
        public int CategoryNumber { get; set; }
        [JsonIgnore]
        public int StudioNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateReleased { get; set; }
        [Column(TypeName = "decimal(28, 0)")]
        public decimal StandardCharge { get; set; }
        [Column(TypeName = "decimal(28, 0)")]
        public decimal? PenaltyCharge { get; set; }

        [ForeignKey("CategoryNumber")]
        [InverseProperty("Dvdtitles")]
        [JsonPropertyName("dvdCategory")]
        public virtual Dvdcategory CategoryNumberNavigation { get; set; }
        [ForeignKey("ProducerNumber")]
        [InverseProperty("Dvdtitles")]
        [JsonPropertyName("dvdProducer")]
        public virtual Producer ProducerNumberNavigation { get; set; }
        [ForeignKey("StudioNumber")]
        [InverseProperty("Dvdtitles")]
        [JsonPropertyName("dvdStudio")]
        public virtual Studio StudioNumberNavigation { get; set; }
        [InverseProperty("DvDnumberNavigation")]
        public virtual ICollection<DvDimage> DvDimages { get; set; }
        
        [InverseProperty("DvdnumberNavigation")]
        [JsonIgnore]
        public virtual ICollection<Dvdcopy> Dvdcopies { get; set; }

        [ForeignKey("DvdNumber")]
        [InverseProperty("DvdNumbers")]
        [JsonPropertyName("actors")]
        public virtual ICollection<Actor> ActorNumbers { get; set; }
    }
}
