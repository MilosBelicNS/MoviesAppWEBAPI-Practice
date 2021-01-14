using Movies.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace Movies.Asp.NetWebApi.Controllers
{
    public class MoviesController : ApiController
    {

        private MoviesDbContext db = new MoviesDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        
        // GET api/movies
      //  EAGER LOADING 
        public IQueryable<Movie> GetAll()
        {
            return db.Movies.Include(b => b.Director);
        }


        // GET api/movie/1
        [ResponseType(typeof(Movie))]
        public IHttpActionResult GetById(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
        // POST api/movies
        [ResponseType(typeof(Movie))]
        public IHttpActionResult Post(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }
        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }

        // PUT api/books/1
        [ResponseType(typeof(Movie))]
        public IHttpActionResult Put(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return Ok(movie);
        }

        // DELETE api/movie/1
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }



    }
}
