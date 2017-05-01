using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoNameHotel.Models
{
    public class Client
    {
        public int Id { get; set; }
       
        [Required]
        [Display(Name="First name")]
        [StringLength(20,ErrorMessage ="The first name must be 20 or less simbols!")]
        [RegularExpression(@"^[A-Z]{1}[a-z]+(-[A-Z]{1}[a-z]+)?$", ErrorMessage ="The first name must start with upper case and contain only letters and -!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(20, ErrorMessage = "The last name must be 20 simbols!")]
        [RegularExpression(@"^[A-Z]{1}[a-z]+(-[A-Z]{1}[a-z]+)?$", ErrorMessage = "The last name must start with upper case and contain only letters and -!")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Tel number")]
        [StringLength(15 , ErrorMessage ="The tel number must be 15 or less digits!")]
        [RegularExpression(@"^(3598|08){1}[7-9]{1}[0-9]{7}$" , ErrorMessage = "The tel number must contain only digits and starts with 3598 or 08!")]
        public string TelNumber { get; set; }

        [Required]
        [StringLength(256,ErrorMessage ="The email address must be 256 symbols or less!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Invalid Email (Example: aaa@abv.bg)")]
        public string Email { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public static implicit operator int(Client v)
        {
            throw new NotImplementedException();
        }
    }
}
