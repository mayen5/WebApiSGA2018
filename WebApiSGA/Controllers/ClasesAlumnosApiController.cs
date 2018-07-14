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
    public class ClasesAlumnosApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public ClasesAlumnosApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/ClasesAlumnosApi
        public IQueryable<ClaseAlumno> GetClasesAlumnos()
        {
            return db.ClasesAlumnos;
        }

        // GET: api/ClasesAlumnosApi/5
        [ResponseType(typeof(ClaseAlumno))]
        public async Task<IHttpActionResult> GetClaseAlumno(int id)
        {
            ClaseAlumno claseAlumno = await db.ClasesAlumnos.FindAsync(id);
            if (claseAlumno == null)
            {
                return NotFound();
            }

            return Ok(claseAlumno);
        }

        // PUT: api/ClasesAlumnosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClaseAlumno(int id, ClaseAlumno claseAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != claseAlumno.AlumnoId)
            {
                return BadRequest();
            }

            db.Entry(claseAlumno).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseAlumnoExists(id))
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

        // POST: api/ClasesAlumnosApi
        [ResponseType(typeof(ClaseAlumno))]
        public async Task<IHttpActionResult> PostClaseAlumno(ClaseAlumno claseAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClasesAlumnos.Add(claseAlumno);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClaseAlumnoExists(claseAlumno.AlumnoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = claseAlumno.AlumnoId }, claseAlumno);
        }

        // DELETE: api/ClasesAlumnosApi/5
        [ResponseType(typeof(ClaseAlumno))]
        public async Task<IHttpActionResult> DeleteClaseAlumno(int id)
        {
            ClaseAlumno claseAlumno = await db.ClasesAlumnos.FindAsync(id);
            if (claseAlumno == null)
            {
                return NotFound();
            }

            db.ClasesAlumnos.Remove(claseAlumno);
            await db.SaveChangesAsync();

            return Ok(claseAlumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClaseAlumnoExists(int id)
        {
            return db.ClasesAlumnos.Count(e => e.AlumnoId == id) > 0;
        }
    }
}