using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoNameHotel.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Number of room")]
        [StringLength(5)]
        public string NumRoom { get; set; }

        [Required]
        [Display(Name = "Number of adults")]
        [Range(0, 8)]
        public int NumberOfAdults { get; set; }

        [Required]
        [Display(Name = "Number of children")]
        [Range(0, 8)]
        public int NumberOfChildren { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
