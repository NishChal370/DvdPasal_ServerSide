using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Models
{
    [Table("DVDCategory")]
    public partial class Dvdcategory
    {
        public Dvdcategory()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        [Key]
        public int CategoryNumber { get; set; }
        

        public string CategoryDescription { get; set; }
        public bool AgeRestricted { get; set; }

        [InverseProperty("CategoryNumberNavigation")]
        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
