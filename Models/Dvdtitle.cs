using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Dvdtitle
    {
        public Dvdtitle()
        {
            Dvdcopies = new HashSet<Dvdcopy>();
            ActorNumbers = new HashSet<Actor>();
        }

        public int Dvdnumber { get; set; }
        public int ProducerNumber { get; set; }
        public int CategoryNumber { get; set; }
        public int StudioNumber { get; set; }
        public DateTime DateReleased { get; set; }
        public decimal StandardCharge { get; set; }
        public decimal? PenaltyCharge { get; set; }

        public virtual Dvdcategory CategoryNumberNavigation { get; set; }
        public virtual Producer ProducerNumberNavigation { get; set; }
        public virtual Studio StudioNumberNavigation { get; set; }
        public virtual ICollection<Dvdcopy> Dvdcopies { get; set; }

        public virtual ICollection<Actor> ActorNumbers { get; set; }
    }
}
