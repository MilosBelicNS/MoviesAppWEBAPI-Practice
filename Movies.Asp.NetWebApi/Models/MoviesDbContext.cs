using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Asp.NetWebApi.Models
{
    public class MoviesDbContext : DbContext
    {

        public MoviesDbContext():base("name=Movies")
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
    }
}