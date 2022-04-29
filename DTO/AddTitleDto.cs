using DvD_Api.Models;
using DvD_Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class AddTitleDto
    {
        [Required]
        public string DvDName { get; set; }


        [Required]
        public DateTime DateReleased { get; set; }

        [Required]
        public decimal StandardCharge { get; set; }

        [Required]
        [LessThan(otherPoperty:nameof(StandardCharge), ErrorMessage = "Penalty cannot be grater than standard charge!")]
        public decimal PenaltyCharge { get; set; }

        [Required]
        public ICollection<DvDimage> DvDImages { get; set; }


        [Required]
        public Producer DvdProducer { get; set; }

        [Required]
        public Studio DvdStudio { get; set; }

        [Required]
        public Dvdcategory Dvdcategory { get; set; }

        [Required]
        public ICollection<Actor> Actors { get; set; }
    }
}
