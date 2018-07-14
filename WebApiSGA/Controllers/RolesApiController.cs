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
    public class RolesApiController : ApiController
    {
        private EjemploEFF1DataContext db = new EjemploEFF1DataContext();

        public RolesApiController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/RolesApi
        public IQueryable<Rol> GetRoles()
        {
            return db.Roles;
        }

        // GET: api/RolesApi/5
        [ResponseType(typeof(Rol))]
        public async Task<IHttpActionResult> GetRol(int id)
        {
            Rol rol = await db.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        // PUT: api/RolesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRol(int id, Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rol.RolId)
            {
                return BadRequest();
            }

            db.Entry(rol).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/RolesApi
        [ResponseType(typeof(Rol))]
        public async Task<IHttpActionResult> PostRol(Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(rol);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rol.RolId }, rol);
        }

        // DELETE: api/RolesApi/5
        [ResponseType(typeof(Rol))]
        public async Task<IHttpActionResult> DeleteRol(int id)
        {
            Rol rol = await db.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            db.Roles.Remove(rol);
            await db.SaveChangesAsync();

            return Ok(rol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolExists(int id)
        {
            return db.Roles.Count(e => e.RolId == id) > 0;
        }
    }
}