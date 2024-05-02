﻿using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
