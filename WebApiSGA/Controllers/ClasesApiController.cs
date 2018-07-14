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
    public class ClasesApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public ClasesApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/ClasesApi
        public IQueryable<Clase> GetClases()
        {
            return db.Clases;
        }

        // GET: api/ClasesApi/5
        [ResponseType(typeof(Clase))]
        public async Task<IHttpActionResult> GetClase(int id)
        {
            Clase clase = await db.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }

            return Ok(clase);
        }

        // PUT: api/ClasesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClase(int id, Clase clase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clase.ClaseId)
            {
                return BadRequest();
            }

            db.Entry(clase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseExists(id))
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

        // POST: api/ClasesApi
        [ResponseType(typeof(Clase))]
        public async Task<IHttpActionResult> PostClase(Clase clase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clases.Add(clase);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = clase.ClaseId }, clase);
        }

        // DELETE: api/ClasesApi/5
        [ResponseType(typeof(Clase))]
        public async Task<IHttpActionResult> DeleteClase(int id)
        {
            Clase clase = await db.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }

            db.Clases.Remove(clase);
            await db.SaveChangesAsync();

            return Ok(clase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClaseExists(int id)
        {
            return db.Clases.Count(e => e.ClaseId == id) > 0;
        }
    }
}