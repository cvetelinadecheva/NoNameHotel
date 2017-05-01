using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoNameHotel.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Picture url")]
        [StringLength(200)]
        public string PictureUrl { get; set; }

        [Required]
        [Display(Name = "Picture url")]
        [StringLength(200)]
        public string PictureUrl2 { get; set; }

        [Required]
        [Display(Name = "Picture url")]
        [StringLength(200)]
        public string PictureUrl3 { get; set; }

        [Required]
        [Display(Name ="Short description")]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name ="Full description")]
        [StringLength(2000)]
        public string FullDescription { get; set; }

        [Required]
        [Display(Name ="Small picture url")]
        [StringLength(200)]
        public string SmallPictureUrl { get;  set; }
    }
}
