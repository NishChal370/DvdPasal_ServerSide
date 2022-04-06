using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Dvdcategory
    {
        public Dvdcategory()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        public int CategoryNumber { get; set; }
        public string CategoryDescription { get; set; }
        public bool AgeRestricted { get; set; }

        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
