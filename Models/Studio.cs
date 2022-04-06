using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Studio
    {
        public Studio()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        public int StudioNumber { get; set; }
        public string StudioName { get; set; }

        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
