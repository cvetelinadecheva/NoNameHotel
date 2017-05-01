using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoNameHotel.Models
{
    public enum Status { New ,  Submitted, Approved };

    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromDate >= ToDate)
            {
                yield return
                  new ValidationResult("EndDate must be greater than StartDate");
            }

            if (FromDate <= DateTime.Now)
            {
                yield return
                  new ValidationResult("Start Date must be greater than today!");
            }
        }

        [Required]
        [Display(Name = "Number of adults")]
        [Range(1,4)]
        public int NumberOfAdults { get; set; }

        [Required]
        [Display(Name = "Number of children")]
        [Range(0, 4)]
        public int NumberOfChildren { get; set; }

        public Status Status { get; set; } = 0;

        [NotMapped]
        public bool Selected { get; set; }

        public int ClientId { get; set; }
        public int RoomId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Room Room { get; set; }
    }
}
