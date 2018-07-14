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
    public class BitacorasApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public BitacorasApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/BitacorasApi
        public IQueryable<Bitacora> GetBitacoras()
        {
            return db.Bitacoras;
        }

        // GET: api/BitacorasApi/5
        [ResponseType(typeof(Bitacora))]
        public async Task<IHttpActionResult> GetBitacora(int id)
        {
            Bitacora bitacora = await db.Bitacoras.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            return Ok(bitacora);
        }

        // PUT: api/BitacorasApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBitacora(int id, Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bitacora.IdTransaccion)
            {
                return BadRequest();
            }

            db.Entry(bitacora).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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

        // POST: api/BitacorasApi
        [ResponseType(typeof(Bitacora))]
        public async Task<IHttpActionResult> PostBitacora(Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bitacoras.Add(bitacora);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bitacora.IdTransaccion }, bitacora);
        }

        // DELETE: api/BitacorasApi/5
        [ResponseType(typeof(Bitacora))]
        public async Task<IHttpActionResult> DeleteBitacora(int id)
        {
            Bitacora bitacora = await db.Bitacoras.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            db.Bitacoras.Remove(bitacora);
            await db.SaveChangesAsync();

            return Ok(bitacora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BitacoraExists(int id)
        {
            return db.Bitacoras.Count(e => e.IdTransaccion == id) > 0;
        }
    }
}