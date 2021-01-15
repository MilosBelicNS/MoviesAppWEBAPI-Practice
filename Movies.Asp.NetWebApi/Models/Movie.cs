using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Asp.NetWebApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Genre { get; set; }
        [Required]
        [Range(1900, 2021)]
        public int Year { get; set; }

        //foreeign key
        public int DirectorId { get; set; }
        //navigation property
        public Director Director { get; set; }
    }
}