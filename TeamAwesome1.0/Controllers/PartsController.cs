using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TeamAwesome1._0.Models;

namespace TeamAwesome1._0.Controllers
{
    public class PartsController : ApiController
    {
        private TeamAwesomeEntities db = new TeamAwesomeEntities();

        // GET: api/Parts
        public IQueryable<Part> GetParts()
        {
            return db.Parts;
        }

        // GET: api/Parts/5
        [ResponseType(typeof(Part))]
        public IHttpActionResult GetPart(string id)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        // PUT: api/Parts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPart(string id, Part part)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != part.PartNo)
            {
                return BadRequest();
            }

            db.Entry(part).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartExists(id))
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

        // POST: api/Parts
        [ResponseType(typeof(Part))]
        public IHttpActionResult PostPart(Part part)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parts.Add(part);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PartExists(part.PartNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = part.PartNo }, part);
        }

        // DELETE: api/Parts/5
        [ResponseType(typeof(Part))]
        public IHttpActionResult DeletePart(string id)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return NotFound();
            }

            db.Parts.Remove(part);
            db.SaveChanges();

            return Ok(part);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartExists(string id)
        {
            return db.Parts.Count(e => e.PartNo == id) > 0;
        }
    }
}