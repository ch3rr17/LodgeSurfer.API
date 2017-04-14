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

namespace LodgeSurfer.API.Controllers
{
    public class MessagesController : ApiController
    {
        private LodgeSurferDataContext db = new LodgeSurferDataContext();

        // GET: api/Messages
        public IHttpActionResult GetMessages()
        {
            var resultSet = db.Messages.Select(m => new
            {
                m.MessageId,
                m.ConversationId,
                m.UserId,
                m.User.Username,
                m.User.FirstName,
                m.User.LastName,
                m.MessageText,
                m.MessageTime,
                m.Subject
            });

            return Ok(resultSet);
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // GET: api/Messages/GetMessageByUser
        [HttpGet]
        [Route("api/Messages/GetMessagesByUser")]
        public IHttpActionResult GetMessagesByUser(int userId)
        {
            var searchedUser = db.Users.FirstOrDefault(u => u.UserId == userId);

            //IQueryable<Message> resultSet = db.Messages.Where(m => m.UserId == searchedUser.UserId);
            IQueryable<Message> resultSet = db.Messages.Where(m => m.Conversation.UserOneId == searchedUser.UserId || m.Conversation.UserTwoId == searchedUser.UserId);


            return Ok(resultSet.Select(m => new
            {
                m.MessageId,
                m.ConversationId,
                m.UserId,
                m.User.Username,
                m.User.FirstName,
                m.User.LastName,
                m.MessageText,
                m.MessageTime,
                m.Subject,
                UserOneFirstName = m.Conversation.UserOne.FirstName,
                UserOneLastName = m.Conversation.UserOne.LastName,
                UserTwoFirstName = m.Conversation.UserTwo.FirstName,
                UserTwoLastName = m.Conversation.UserTwo.LastName
            }));
        }

        // GET: api/Messages/GetMessageByConvo
        [HttpGet]
        [Route("api/Messages/GetMessagesByConvo")]
        public IHttpActionResult GetMessagesByConvo(int convoId)
        {

            IQueryable<Message> resultSet = db.Messages.Where(m => m.ConversationId == convoId);

            return Ok(resultSet.Select(m => new
            {
                m.MessageId,
                m.ConversationId,
                m.UserId,
                m.User.Username,
                m.User.FirstName,
                m.User.LastName,
                m.MessageText,
                m.MessageTime,
                m.Subject,
                UserOneFirstName = m.Conversation.UserOne.FirstName,
                UserOneLastName = m.Conversation.UserOne.LastName,
                UserTwoFirstName = m.Conversation.UserTwo.FirstName,
                UserTwoLastName = m.Conversation.UserTwo.LastName
            }));
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.MessageId)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }
    }
}