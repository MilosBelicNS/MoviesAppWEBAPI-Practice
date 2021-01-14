using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Asp.NetWebApi.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }

    }
}