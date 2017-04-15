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
using LodgeSurfer.API.Data;
using LodgeSurfer.API.Models;
using System.Web.UI.WebControls;
using System.Web.Http.Cors;

namespace LodgeSurfer.API.Controllers
{
    //[EnableCors("http://localhost:8080", "*", "*")]
    public class UsersController : ApiController
    {
        private LodgeSurferDataContext db = new LodgeSurferDataContext();

        // GET: api/Users
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            //return db.Users;

            var resultSet = db.Users.Select(u => new
            {
                u.UserId,
                u.FirstName,
                u.LastName,
                u.EmailAddress,
                u.ZipCode,
                u.ContactPhone,
                u.BirthDate,
                u.Username
            });

            return Ok(resultSet);

            }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var dbUsers = db.Users.Find(id);

            //Map it to an anonymous object (To filter the columns)
            var mappedListing = new
            {
                dbUsers.UserId,
                dbUsers.FirstName,
                dbUsers.LastName,
                dbUsers.EmailAddress,
                dbUsers.ZipCode,
                dbUsers.ContactPhone,
                dbUsers.BirthDate,
                dbUsers.Username
            };

            //Return the mappedListing (the one with filtered columns)
            return Ok(mappedListing);

        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

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
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        //POST Login user
        [Route("api/Users/Login")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult Login(Models.Login login)
        {
            var resultSet = db.Users.Where(u => u.EmailAddress == login.EmailAddress && u.Password == login.Password);

            return Ok(resultSet);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}