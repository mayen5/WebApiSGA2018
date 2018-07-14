using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EjemploEEF1.Model;

namespace WebApiSGA.Controllers
{
    public class CarrerasApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public CarrerasApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/CarrerasApi
        public IQueryable<Carrera> GetCarreras()
        {
            return db.Carreras;
        }

        // GET: api/CarrerasApi/5
        [ResponseType(typeof(Carrera))]
        public async Task<IHttpActionResult> GetCarrera(int id)
        {
            Carrera carrera = await db.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            return Ok(carrera);
        }

        // PUT: api/CarrerasApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarrera(int id, Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrera.CarreraId)
            {
                return BadRequest();
            }

            db.Entry(carrera).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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

        // POST: api/CarrerasApi
        [ResponseType(typeof(Carrera))]
        public async Task<IHttpActionResult> PostCarrera(Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carreras.Add(carrera);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = carrera.CarreraId }, carrera);
        }

        // DELETE: api/CarrerasApi/5
        [ResponseType(typeof(Carrera))]
        public async Task<IHttpActionResult> DeleteCarrera(int id)
        {
            Carrera carrera = await db.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            db.Carreras.Remove(carrera);
            await db.SaveChangesAsync();

            return Ok(carrera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarreraExists(int id)
        {
            return db.Carreras.Count(e => e.CarreraId == id) > 0;
        }
    }
}