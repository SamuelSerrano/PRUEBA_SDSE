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
    public class ClimaCiudadesController : ApiController
    {
        private bd_climaEntities db = new bd_climaEntities();

        // GET: api/ClimaCiudades
        public IQueryable<ClimaCiudad> GetClimaCiudad()
        {
            
            return db.ClimaCiudad;
        }

        // GET: api/ClimaCiudades/5
        [ResponseType(typeof(ClimaCiudad))]
        public IHttpActionResult GetClimaCiudad(int id)
        {
            ClimaCiudad climaCiudad = db.ClimaCiudad.Find(id);
            if (climaCiudad == null)
            {
                return NotFound();
            }

            return Ok(climaCiudad);
        }

        // PUT: api/ClimaCiudades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClimaCiudad(int id, ClimaCiudad climaCiudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != climaCiudad.id)
            {
                return BadRequest();
            }

            db.Entry(climaCiudad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClimaCiudadExists(id))
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

        // POST: api/ClimaCiudades
        [ResponseType(typeof(ClimaCiudad))]
        public IHttpActionResult PostClimaCiudad(ClimaCiudad climaCiudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClimaCiudad.Add(climaCiudad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = climaCiudad.id }, climaCiudad);
        }

        // DELETE: api/ClimaCiudades/5
        [ResponseType(typeof(ClimaCiudad))]
        public IHttpActionResult DeleteClimaCiudad(int id)
        {
            ClimaCiudad climaCiudad = db.ClimaCiudad.Find(id);
            if (climaCiudad == null)
            {
                return NotFound();
            }

            db.ClimaCiudad.Remove(climaCiudad);
            db.SaveChanges();

            return Ok(climaCiudad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClimaCiudadExists(int id)
        {
            return db.ClimaCiudad.Count(e => e.id == id) > 0;
        }
    }
}