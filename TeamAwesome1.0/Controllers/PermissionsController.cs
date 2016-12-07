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
    public class PermissionsController : ApiController
    {
        private TeamAwesomeEntities db = new TeamAwesomeEntities();

        // GET: api/Permissions
        public IQueryable<Permission> GetPermissions()
        {
            return db.Permissions;
        }

        // GET: api/Permissions/5
        [ResponseType(typeof(PermissionViewModel))]
        public IHttpActionResult GetPermission(string id)
        {
            int? UserTypeNo = db.Users.Select(x => x).Where(x => x.LoginID == id).First().UserTypeNo;
            if (UserTypeNo == null)
            {
                NotFound();
            }

            Permission permission = db.Permissions.Find(UserTypeNo);
            PermissionViewModel p = new PermissionViewModel() {
                UserTypeNo = permission.UserTypeNo,
                UserTypeName = permission.UserTypeName,
                AR = permission.AR,
                PR = permission.PR
            };

            //int ? userTypeNo = db.Users.Find(id).UserTypeNo;
            //if (userTypeNo == null) {
            //    return NotFound();
            //}

            //Permission p = db.Permissions.Find(userTypeNo);
            //if (permission == null)
            //{
            //    return NotFound();
            //}

            return Ok(p);
        }

        // PUT: api/Permissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermission(int id, Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permission.UserTypeNo)
            {
                return BadRequest();
            }

            db.Entry(permission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/Permissions
        [ResponseType(typeof(Permission))]
        public IHttpActionResult PostPermission(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permissions.Add(permission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PermissionExists(permission.UserTypeNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = permission.UserTypeNo }, permission);
        }

        // DELETE: api/Permissions/5
        [ResponseType(typeof(Permission))]
        public IHttpActionResult DeletePermission(int id)
        {
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            db.Permissions.Remove(permission);
            db.SaveChanges();

            return Ok(permission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(int id)
        {
            return db.Permissions.Count(e => e.UserTypeNo == id) > 0;
        }
    }
}