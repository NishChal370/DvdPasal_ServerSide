using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Dvdnumbers = new HashSet<Dvdtitle>();
        }

        public int ActorNumber { get; set; }
        public string ActorName { get; set; }
        public string ActorLastName { get; set; }

        public virtual ICollection<Dvdtitle> Dvdnumbers { get; set; }
    }
}
