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
    public class GruposAcademicosApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();
        public GruposAcademicosApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/GruposAcademicosApi
        public IQueryable<GrupoAcademico> GetGruposAcademicos()
        {
            return db.GruposAcademicos;
        }

        // GET: api/GruposAcademicosApi/5
        [ResponseType(typeof(GrupoAcademico))]
        public async Task<IHttpActionResult> GetGrupoAcademico(int id)
        {
            GrupoAcademico grupoAcademico = await db.GruposAcademicos.FindAsync(id);
            if (grupoAcademico == null)
            {
                return NotFound();
            }

            return Ok(grupoAcademico);
        }

        // PUT: api/GruposAcademicosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrupoAcademico(int id, GrupoAcademico grupoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupoAcademico.GrupoAcademicoId)
            {
                return BadRequest();
            }

            db.Entry(grupoAcademico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoAcademicoExists(id))
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

        // POST: api/GruposAcademicosApi
        [ResponseType(typeof(GrupoAcademico))]
        public async Task<IHttpActionResult> PostGrupoAcademico(GrupoAcademico grupoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GruposAcademicos.Add(grupoAcademico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = grupoAcademico.GrupoAcademicoId }, grupoAcademico);
        }

        // DELETE: api/GruposAcademicosApi/5
        [ResponseType(typeof(GrupoAcademico))]
        public async Task<IHttpActionResult> DeleteGrupoAcademico(int id)
        {
            GrupoAcademico grupoAcademico = await db.GruposAcademicos.FindAsync(id);
            if (grupoAcademico == null)
            {
                return NotFound();
            }

            db.GruposAcademicos.Remove(grupoAcademico);
            await db.SaveChangesAsync();

            return Ok(grupoAcademico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoAcademicoExists(int id)
        {
            return db.GruposAcademicos.Count(e => e.GrupoAcademicoId == id) > 0;
        }
    }
}