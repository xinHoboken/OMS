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
using TeamAwesome1._0.ViewModels;

namespace TeamAwesome1._0.Controllers
{
    public class LaborsController : ApiController
    {
        private TeamAwesomeEntities db = new TeamAwesomeEntities();

        // GET: api/Labors
        public IQueryable<LaborViewModel> GetLabors()
        {
            return from l in db.Labors
                   join u in db.Users on l.EmpID equals u.EmpID
                   //join p in db.Parts on l.PartNo equals p.PartNo
                   select new LaborViewModel {
                       EmpID = l.EmpID,
                       LaborNo = l.LaborNo,
                       OrderNo = l.OrderNo,
                       PartNo = l.PartNo,
                       Hours = l.Hours,
                       Mins = l.Mins,
                       FName = u.FName,
                       LName = u.LName
                   };

        }

        // GET: api/Labors/5
        [ResponseType(typeof(Labor))]
        public IHttpActionResult GetLabor(int id)
        {
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return NotFound();
            }

            return Ok(labor);
        }

        // PUT: api/Labors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLabor(int id, Labor labor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != labor.LaborNo)
            {
                return BadRequest();
            }

            db.Entry(labor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaborExists(id))
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

        // POST: api/Labors
        [ResponseType(typeof(LaborViewModel))]
        public IHttpActionResult PostLabor(LaborViewModel labor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Labor NewLabor = new Labor();
            NewLabor.EmpID = labor.EmpID;
            NewLabor.OrderNo = labor.OrderNo;
            NewLabor.PartNo = labor.PartNo;
            NewLabor.Hours = labor.Hours;
            NewLabor.Mins = labor.Mins;
            db.Labors.Add(NewLabor);

            db.SaveChanges();

            labor.FName = db.Users.Find(labor.EmpID).FName;
            labor.LName = db.Users.Find(labor.EmpID).LName;
            return CreatedAtRoute("DefaultApi", new { id = labor.LaborNo }, labor);
        }

        // DELETE: api/Labors/5
        [ResponseType(typeof(Labor))]
        public IHttpActionResult DeleteLabor(int id)
        {
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return NotFound();
            }

            db.Labors.Remove(labor);
            db.SaveChanges();

            return Ok(labor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LaborExists(int id)
        {
            return db.Labors.Count(e => e.LaborNo == id) > 0;
        }
    }
}