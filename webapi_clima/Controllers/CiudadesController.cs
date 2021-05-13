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
    public class CiudadesController : ApiController
    {
        private bd_climaEntities db = new bd_climaEntities();

        // GET: api/Ciudades
        public IQueryable<Ciudades> GetCiudades()
        {
            return db.Ciudades;
        }

        // GET: api/Ciudades/5
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult GetCiudades(int id)
        {
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return Ok(ciudades);
        }

        // PUT: api/Ciudades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiudades(int id, Ciudades ciudades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ciudades.id)
            {
                return BadRequest();
            }

            db.Entry(ciudades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadesExists(id))
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

        // POST: api/Ciudades
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult PostCiudades(Ciudades ciudades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ciudades.Add(ciudades);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ciudades.id }, ciudades);
        }

        // DELETE: api/Ciudades/5
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult DeleteCiudades(int id)
        {
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return NotFound();
            }

            db.Ciudades.Remove(ciudades);
            db.SaveChanges();

            return Ok(ciudades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CiudadesExists(int id)
        {
            return db.Ciudades.Count(e => e.id == id) > 0;
        }
    }
}