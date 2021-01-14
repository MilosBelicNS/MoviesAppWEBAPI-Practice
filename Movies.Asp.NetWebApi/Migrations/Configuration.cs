namespace Movies.Asp.NetWebApi.Migrations
{
    using Movies.Asp.NetWebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movies.Asp.NetWebApi.Models.MoviesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Movies.Asp.NetWebApi.Models.MoviesDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            context.Directors.AddOrUpdate(x => x.Id,
                new Director() { Id = 1, Name = "Dragan", Surname= "Bjelogrlic", Birth = new DateTime(1963,10,10).Date },
                new Director() { Id = 2, Name = "Emir", Surname= "Kusturica", Birth = new DateTime(1954,11,24).Date },
                new Director() { Id = 3, Name = "Dusan", Surname="Kovacevic", Birth= new DateTime(1948,7,12).Date }
                );


            context.Movies.AddOrUpdate(x => x.Id,
                new Movie()
                {
                    Id = 1,
                    Name = "Montevideo, Bog te video!",
                    Genre = "Drama/Comedy",
                    Year = 2012,
                    DirectorId = 1

                },
                new Movie()
                {
                    Id = 2,
                    Name = "Underground",
                    Genre = "Drama/War",
                    Year = 1995,
                    DirectorId = 2
                },
                new Movie()
                {
                    Id = 3,
                    Name = "Balkanski spijun",
                    Genre = "Drama/Comedy",
                    Year = 1984,
                    DirectorId = 3
                });
        }
    }
}
