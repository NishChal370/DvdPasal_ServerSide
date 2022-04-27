using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(100)]
        
        public string DvdName { get; set; }
        public int ProducerNumber { get; set; }
        public int CategoryNumber { get; set; }
        public int StudioNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateReleased { get; set; }
        [Column(TypeName = "decimal(28, 0)")]
        public decimal StandardCharge { get; set; }
        [Column(TypeName = "decimal(28, 0)")]
        public decimal? PenaltyCharge { get; set; }

        [ForeignKey("CategoryNumber")]
        [InverseProperty("Dvdtitles")]
        public virtual Dvdcategory CategoryNumberNavigation { get; set; }
        [ForeignKey("ProducerNumber")]
        [InverseProperty("Dvdtitles")]
        public virtual Producer ProducerNumberNavigation { get; set; }
        [ForeignKey("StudioNumber")]
        [InverseProperty("Dvdtitles")]
        public virtual Studio StudioNumberNavigation { get; set; }
        [InverseProperty("DvDnumberNavigation")]
        public virtual ICollection<DvDimage> DvDimages { get; set; }
        [InverseProperty("DvdnumberNavigation")]
        public virtual ICollection<Dvdcopy> Dvdcopies { get; set; }

        [ForeignKey("DvdNumber")]
        [InverseProperty("DvdNumbers")]
        public virtual ICollection<Actor> ActorNumbers { get; set; }
    }
}
