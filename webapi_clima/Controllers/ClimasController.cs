using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webapi_clima.Models;

namespace webapi_clima.Controllers
{
    public class ClimasController : ApiController
    {
        private bd_climaEntities db = new bd_climaEntities();

        // GET: api/Climas
        public IQueryable<Climas> GetClimas()
        {
            return db.Climas;
        }

        // GET: api/Climas/5
        [ResponseType(typeof(Climas))]
        public IHttpActionResult GetClimas(int id)
        {
            Climas climas = db.Climas.Find(id);
            if (climas == null)
            {
                return NotFound();
            }

            return Ok(climas);
        }

        // PUT: api/Climas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClimas(int id, Climas climas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != climas.id)
            {
                return BadRequest();
            }

            db.Entry(climas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClimasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Climas
        [ResponseType(typeof(Climas))]
        public IHttpActionResult PostClimas(Climas climas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Climas.Add(climas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = climas.id }, climas);
        }

        // DELETE: api/Climas/5
        [ResponseType(typeof(Climas))]
        public IHttpActionResult DeleteClimas(int id)
        {
            Climas climas = db.Climas.Find(id);
            if (climas == null)
            {
                return NotFound();
            }

            db.Climas.Remove(climas);
            db.SaveChanges();

            return Ok(climas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClimasExists(int id)
        {
            return db.Climas.Count(e => e.id == id) > 0;
        }
    }
}