using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Asp.NetWebApi.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "Birth date must be in past tense!")]
        public DateTime Birth { get; set; }

    }
}