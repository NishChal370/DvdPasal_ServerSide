using DvD_Api.Models;
using DvD_Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace DvD_Api.DTO
{
    public class AddTitleDto
    {
        [Required(ErrorMessage = "DvD Name is required")]
        public string DvDName { get; set; }


        [Required(ErrorMessage = "Date released is required")]
        public DateTime DateReleased { get; set; }

        [Required(ErrorMessage = "Standard Price is required.")]
        public decimal StandardCharge { get; set; }

        [Required(ErrorMessage ="Penalty Rate is required")]
        [LessThan(otherPoperty:nameof(StandardCharge), ErrorMessage = "Penalty cannot be grater than standard charge!")]
        public decimal PenaltyCharge { get; set; }

        [Required(ErrorMessage = "DvD Images are required.")]
        public ICollection<DvDimage> DvDImages { get; set; }


        [Required(ErrorMessage = "Producer is required")]
        public Producer DvdProducer { get; set; }

        [Required(ErrorMessage = "Studio is required")]
        public Studio DvdStudio { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Dvdcategory Dvdcategory { get; set; }

        [Required(ErrorMessage = "Actors are required.")]
        public ICollection<Actor> Actors { get; set; }
    }
}
