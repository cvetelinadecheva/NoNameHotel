using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoNameHotel.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(200)]
        public string Picture { get; set; }

        [Required]
        [StringLength(200)]
        public string Picture2 { get; set; }

        [Required]
        [StringLength(200)]
        public string Picture3 { get; set; }

        [Required]
        [StringLength(200)]
        public string Picture4 { get; set; }

        [Required]
        [Range(0,300)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
