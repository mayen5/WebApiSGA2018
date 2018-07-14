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
    public class AlumnosApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public AlumnosApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/AlumnosApi
        public IQueryable<Alumno> GetAlumnos()
        {
            return db.Alumnos;
        }

        // GET: api/AlumnosApi/5
        [ResponseType(typeof(Alumno))]
        public async Task<IHttpActionResult> GetAlumno(int id)
        {
            Alumno alumno = await db.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            return Ok(alumno);
        }

        // PUT: api/AlumnosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno.AlumnoId)
            {
                return BadRequest();
            }

            db.Entry(alumno).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        // POST: api/AlumnosApi
        [ResponseType(typeof(Alumno))]
        public async Task<IHttpActionResult> PostAlumno(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alumnos.Add(alumno);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alumno.AlumnoId }, alumno);
        }

        // DELETE: api/AlumnosApi/5
        [ResponseType(typeof(Alumno))]
        public async Task<IHttpActionResult> DeleteAlumno(int id)
        {
            Alumno alumno = await db.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            db.Alumnos.Remove(alumno);
            await db.SaveChangesAsync();

            return Ok(alumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumnoExists(int id)
        {
            return db.Alumnos.Count(e => e.AlumnoId == id) > 0;
        }
    }
}