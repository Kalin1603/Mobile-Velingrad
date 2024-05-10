using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            EmailConfirmed = true;
            this.Vehicles = new HashSet<Vehicle>();
        }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(0)]
        [Display(Name = "Identification number")]
        public string NationalId { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
