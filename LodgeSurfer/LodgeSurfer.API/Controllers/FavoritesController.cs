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
    public class FavoritesController : ApiController
    {
        private LodgeSurferDataContext db = new LodgeSurferDataContext();

        // GET: api/Favorites
        public IHttpActionResult GetFavorites()
        {
            var resultSet = db.Favorites.Select(f => new
            {
                f.UserId,
                f.ListingId
            });

            return Ok(resultSet);
        }

        // GET: api/Favorites/5
        [ResponseType(typeof(Favorite))]
        public IHttpActionResult GetFavorite(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }

        //GET: api/Listings/SearchFavorites
        [Route("api/Listings/SearchFavorites/{userId}")]
        public IHttpActionResult GetSearchFavorites(int userId)
        {
            var resultSet = db.Favorites.Where(f => f.UserId == userId);

            return Ok(resultSet.Select(f => new
            {
                f.Listing.ListingName,
                f.Listing.Address1,
                f.Listing.Address2,
                f.Listing.City,
                f.Listing.State,
                f.Listing.ZipCode,
                f.Listing.ContactPhone,
                f.Listing.Price,
                f.Listing.ListingDescription,
                f.Listing.Bedroom,
                f.Listing.Bathroom,
                f.Listing.ListingImage
            }));

        }

        // PUT: api/Favorites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFavorite(int id, Favorite favorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favorite.UserId)
            {
                return BadRequest();
            }

            db.Entry(favorite).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        [ResponseType(typeof(Favorite))]
        public IHttpActionResult PostFavorite(Favorite favorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Favorites.Add(favorite);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FavoriteExists(favorite.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = favorite.UserId }, favorite);
        }
        
        [HttpDelete]
        [Route("api/Favorites/{userId}/Favorites/{listingId}")]
        public IHttpActionResult DeleteFavorite(int userId, int listingId)
        {
            //Favorite favorite = db.Favorites.FirstOrDefault
            Favorite favorite = db.Favorites.Find(userId, listingId);

            if(favorite == null)
            {
                return NotFound();
            }

            db.Favorites.Remove(favorite);
            db.SaveChanges();

            return Ok(favorite);
        }

        // DELETE: api/Favorites/5
        [ResponseType(typeof(Favorite))]
        public IHttpActionResult DeleteFavorite(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return NotFound();
            }

            db.Favorites.Remove(favorite);
            db.SaveChanges();

            return Ok(favorite);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FavoriteExists(int id)
        {
            return db.Favorites.Count(e => e.UserId == id) > 0;
        }
    }
}