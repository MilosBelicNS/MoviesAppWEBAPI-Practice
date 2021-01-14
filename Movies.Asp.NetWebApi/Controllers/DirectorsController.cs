using Movies.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Movies.Asp.NetWebApi.Controllers
{
    public class DirectorsController : ApiController
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

        //GET api/directors
        public IQueryable<Director> GetDirectors()
        {
            return db.Directors;
        }

        // GET api/directors/1
        [ResponseType(typeof(Director))]
        public IHttpActionResult GetDirector(int id)
        {
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return NotFound();
            }

            return Ok(director);
        }


        // POST api/directors
        [ResponseType(typeof(Director))]
        public IHttpActionResult PostDirector(Director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Directors.Add(director);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = director.Id }, director);
        }

        private bool DirectorExists(int id)
        {
            return db.Directors.Count(e => e.Id == id) > 0;
        }

        // PUT api/director/1
        [ResponseType(typeof(Director))]
        public IHttpActionResult PutDirector(int id, Director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != director.Id)
            {
                return BadRequest();
            }

            db.Entry(director).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound();
                }
                else
                {

                    return BadRequest();
                   // throw; -> gledamo da uvek izbegnemo ovakvu praksu, time sto cemo samo staviti bad request umesto da mu prosledimo ex
                }
            }

            return Ok(director);
        }

        // DELETE api/director/1
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDirector(int id)
        {
            Director director = db.Directors.Find(id);

            if (director == null)
            {
                return NotFound();
            }

            db.Directors.Remove(director);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
