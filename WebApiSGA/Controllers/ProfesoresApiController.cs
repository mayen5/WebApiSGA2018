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
    public class ProfesoresApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public ProfesoresApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/ProfesoresApi
        public IQueryable<Profesor> GetProfedores()
        {
            return db.Profedores;
        }

        // GET: api/ProfesoresApi/5
        [ResponseType(typeof(Profesor))]
        public async Task<IHttpActionResult> GetProfesor(int id)
        {
            Profesor profesor = await db.Profedores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            return Ok(profesor);
        }

        // PUT: api/ProfesoresApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfesor(int id, Profesor profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesor.ProfesorId)
            {
                return BadRequest();
            }

            db.Entry(profesor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // POST: api/ProfesoresApi
        [ResponseType(typeof(Profesor))]
        public async Task<IHttpActionResult> PostProfesor(Profesor profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Profedores.Add(profesor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profesor.ProfesorId }, profesor);
        }

        // DELETE: api/ProfesoresApi/5
        [ResponseType(typeof(Profesor))]
        public async Task<IHttpActionResult> DeleteProfesor(int id)
        {
            Profesor profesor = await db.Profedores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            db.Profedores.Remove(profesor);
            await db.SaveChangesAsync();

            return Ok(profesor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesorExists(int id)
        {
            return db.Profedores.Count(e => e.ProfesorId == id) > 0;
        }
    }
}