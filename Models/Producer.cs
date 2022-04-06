using System;
using System.Collections.Generic;

namespace DvD_Api.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Dvdtitles = new HashSet<Dvdtitle>();
        }

        public int ProducerNumber { get; set; }
        public string ProducerName { get; set; }

        public virtual ICollection<Dvdtitle> Dvdtitles { get; set; }
    }
}
