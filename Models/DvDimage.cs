using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("DvDImages")]
    public partial class DvDimage
    {
        [Key]
        [Column("DvDImageId")]
        public int DvDimageId { get; set; }
        public int DvDnumber { get; set; }
        
        
        public string Image64 { get; set; }

        [ForeignKey("DvDnumber")]
        [InverseProperty("DvDimages")]
        [JsonIgnore]
        public virtual Dvdtitle DvDnumberNavigation { get; set; }
    }
}
