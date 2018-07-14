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
    public class PuestosApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public PuestosApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/PuestosApi
        public IQueryable<Puesto> GetPuestos()
        {
            return db.Puestos.Include("Profesores");
        }

        // GET: api/PuestosApi/5
        [ResponseType(typeof(Puesto))]
        public async Task<IHttpActionResult> GetPuesto(int id)
        {
            Puesto puesto = await db.Puestos.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // PUT: api/PuestosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPuesto(int id, Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puesto.PuestoId)
            {
                return BadRequest();
            }

            db.Entry(puesto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(id))
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

        // POST: api/PuestosApi
        [ResponseType(typeof(Puesto))]
        public async Task<IHttpActionResult> PostPuesto(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puestos.Add(puesto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = puesto.PuestoId }, puesto);
        }

        // DELETE: api/PuestosApi/5
        [ResponseType(typeof(Puesto))]
        public async Task<IHttpActionResult> DeletePuesto(int id)
        {
            Puesto puesto = await db.Puestos.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }

            db.Puestos.Remove(puesto);
            await db.SaveChangesAsync();

            return Ok(puesto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuestoExists(int id)
        {
            return db.Puestos.Count(e => e.PuestoId == id) > 0;
        }
    }
}