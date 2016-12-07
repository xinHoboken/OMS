using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Http.Cors;
using System.Web.Http.Description;
using TeamAwesome1._0.Models;
using TeamAwesome1._0.ViewModels;

namespace TeamAwesome1._0.Controllers
{
    public class UsersController : ApiController
    {
        private TeamAwesomeEntities db = new TeamAwesomeEntities();

        // GET: api/Users
        public IQueryable<UserViewModel> GetUsers()
        {
            return from u in db.Users join l in db.Logins on u.LoginID equals l.LoginID
                   join p in db.Permissions on u.UserTypeNo equals p.UserTypeNo
                   join d in db.Departments on u.DeptNo equals d.DeptNo
                   select new UserViewModel {
                       EmpID = u.EmpID,
                       FName = u.FName,
                       LName = u.LName,
                       Desc = u.Desc,
                       DeptNo = u.DeptNo,
                       DeptName = d.DeptName,
                       LoginID = u.LoginID,
                       Password = l.Password,
                       UserTypeNo = u.UserTypeNo,
                       UserTypeName = p.UserTypeName,
                       AR = p.AR,
                       PR = p.PR
                   };
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [Route("api/Users/put/{id}")]
        [HttpPost]
        [EnableCors(origins: "http://localhost:53145", headers: "*", methods: "*")]
        public IHttpActionResult PutUser(string id, UserViewModel user)
        {
            User newUser = new User();
            newUser.EmpID = user.EmpID;
            newUser.FName = user.FName;
            newUser.LName = user.LName;
            newUser.Desc = user.Desc;
            newUser.DeptNo = user.DeptNo;
            newUser.LoginID = user.LoginID;
            newUser.UserTypeNo = user.UserTypeNo;

            Login newLogin = new Login();
            newLogin.LoginID = user.LoginID;
            newLogin.Password = user.Password;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.EmpID)
            {
                return BadRequest();
            }

            db.Entry(newUser).State = EntityState.Modified;
            db.Entry(newLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult PostUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Login newLogin = new Login();
            newLogin.LoginID = user.LoginID;
            newLogin.Password = user.Password;
            db.Logins.Add(newLogin);

            User newUser = new User();
            newUser.EmpID = user.EmpID;
            newUser.FName = user.FName;
            newUser.LName = user.LName;
            newUser.Desc = user.Desc;
            newUser.UserTypeNo = user.UserTypeNo;
            newUser.DeptNo = user.DeptNo;
            newUser.LoginID = user.LoginID;

            db.Users.Add(newUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.EmpID) || db.Logins.Count(e => e.LoginID == user.LoginID) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.EmpID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [Route("api/Users/delete/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            List<Labor> labors = db.Labors.Select(x => x).Where(x => x.EmpID == user.EmpID).ToList();
            if (labors != null)
            {
                foreach (var l in labors) {
                    db.Labors.Remove(l);
                }
            }

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.EmpID == id) > 0;
        }
    }
}