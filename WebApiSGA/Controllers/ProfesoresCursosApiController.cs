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
    public class ProfesoresCursosApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public ProfesoresCursosApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/ProfesoresCursosApi
        public IQueryable<ProfesorCurso> GetProfesoresCursos()
        {
            return db.ProfesoresCursos;
        }

        // GET: api/ProfesoresCursosApi/5
        [ResponseType(typeof(ProfesorCurso))]
        public async Task<IHttpActionResult> GetProfesorCurso(int id)
        {
            ProfesorCurso profesorCurso = await db.ProfesoresCursos.FindAsync(id);
            if (profesorCurso == null)
            {
                return NotFound();
            }

            return Ok(profesorCurso);
        }

        // PUT: api/ProfesoresCursosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfesorCurso(int id, ProfesorCurso profesorCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesorCurso.CursoId)
            {
                return BadRequest();
            }

            db.Entry(profesorCurso).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorCursoExists(id))
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

        // POST: api/ProfesoresCursosApi
        [ResponseType(typeof(ProfesorCurso))]
        public async Task<IHttpActionResult> PostProfesorCurso(ProfesorCurso profesorCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfesoresCursos.Add(profesorCurso);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfesorCursoExists(profesorCurso.CursoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profesorCurso.CursoId }, profesorCurso);
        }

        // DELETE: api/ProfesoresCursosApi/5
        [ResponseType(typeof(ProfesorCurso))]
        public async Task<IHttpActionResult> DeleteProfesorCurso(int id)
        {
            ProfesorCurso profesorCurso = await db.ProfesoresCursos.FindAsync(id);
            if (profesorCurso == null)
            {
                return NotFound();
            }

            db.ProfesoresCursos.Remove(profesorCurso);
            await db.SaveChangesAsync();

            return Ok(profesorCurso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesorCursoExists(int id)
        {
            return db.ProfesoresCursos.Count(e => e.CursoId == id) > 0;
        }
    }
}