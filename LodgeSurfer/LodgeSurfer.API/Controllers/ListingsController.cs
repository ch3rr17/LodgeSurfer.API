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
    public class ListingsController : ApiController
    {
        private LodgeSurferDataContext db = new LodgeSurferDataContext();

        // GET: api/Listings
        public IHttpActionResult GetListings()
        {
            var resultSet = db.Listings.Select(l => new
            {
                l.ListingId,
                l.UserId,
                l.User.FirstName,
                l.User.LastName,
                l.ListingName,
                l.Address1,
                l.Address2,
                l.City,
                l.State,
                l.ZipCode,
                l.ContactPhone,
                l.Price,
                l.ListingDescription,
                l.Bedroom,
                l.Bathroom,
                l.ListingImage
            });

            return Ok(resultSet);
        }

        // GET: api/Listings/5
        [ResponseType(typeof(Listing))]
        public IHttpActionResult GetListing(int id)
        {
            // Get the listing from SQL
            var dbListing = db.Listings.Find(id);

            // Map it to an anonymous object (To filter the columns)
            var mappedListing = new
            {
                dbListing.ListingId,
                dbListing.UserId,
                dbListing.User.FirstName,
                dbListing.User.LastName,
                dbListing.ListingName,
                dbListing.Address1,
                dbListing.Address2,
                dbListing.City,
                dbListing.State,
                dbListing.ZipCode,
                dbListing.ContactPhone,
                dbListing.Price,
                dbListing.ListingDescription,
                dbListing.Bedroom,
                dbListing.Bathroom,
                dbListing.ListingImage
            };

            // Return the mappedListing (the one with filtered columns)
            return Ok(mappedListing);
        }

        //Search listings by username
        //GET: api/Listings/GetSearchListingsByUser
        [HttpGet]
        [Route("api/Listings/GetSearchListingsByUser")]
        public IHttpActionResult GetSearchListingsByUser(int userId)
        {
            var searchedUser = db.Users.FirstOrDefault(u => u.UserId == userId);

            IQueryable<Listing> resultSet = db.Listings.Where(l => l.UserId == searchedUser.UserId);

            return Ok(resultSet.Select(l => new
            {
                l.UserId,
                l.User.FirstName,
                l.User.LastName,
                l.User.Username,
                l.ListingName,
                l.ListingId,
                l.Address1,
                l.Address2,
                l.City,
                l.State,
                l.ZipCode,
                l.ContactPhone,
                l.Price,
                l.ListingDescription,
                l.Bedroom,
                l.Bathroom,
                l.ListingImage
            }));
        }

        //Users can search for a listing
        //GET: api/Listings/SearchListings
        [HttpGet]
        [Route("api/Listings/SearchListings")]
        public IHttpActionResult GetSearchListings([FromUri] ListingSearch search)
        {
            IQueryable<Listing> resultSet = db.Listings;


            if (!string.IsNullOrEmpty(search.City))
            {
                resultSet = resultSet.Where(l => l.City == search.City);
            }

            if (!string.IsNullOrEmpty(search.ZipCode))
            {
                resultSet = resultSet.Where(l => l.ZipCode == search.ZipCode);
            }

            if (search.MinimumPrice > 0 && search.MaximumPrice > 0)
            {
                resultSet = resultSet.Where(l => l.Price >= search.MinimumPrice && l.Price <= search.MaximumPrice);
            }
            else if (search.MinimumPrice > 0 || search.MaximumPrice > 0)
            {
                resultSet = resultSet.Where(l => l.Price >= search.MinimumPrice || l.Price <= search.MaximumPrice);
            }

            if (search.Bedroom > 0)
            {
                resultSet = resultSet.Where(l => l.Bedroom == search.Bedroom);
            }
            if (search.Bathroom > 0)
            {
                resultSet = resultSet.Where(l => l.Bathroom == search.Bathroom);
            }

            return Ok(resultSet.Select(l => new
            {
                l.ListingId,
                l.UserId,
                l.User.FirstName,
                l.User.LastName,
                l.ListingName,
                l.Address1,
                l.Address2,
                l.City,
                l.State,
                l.ZipCode,
                l.ContactPhone,
                l.Price,
                l.ListingDescription,
                l.Bedroom,
                l.Bathroom,
                l.ListingImage

            }));
        }
        
        // PUT: api/Listings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutListing(int id, Listing listing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listing.ListingId)
            {
                return BadRequest();
            }

            db.Entry(listing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/Listings
        [ResponseType(typeof(Listing))]
        public IHttpActionResult PostListing(Listing listing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Listings.Add(listing);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = listing.ListingId }, listing);
        }
                
        // DELETE: api/Listings/5
        [ResponseType(typeof(Listing))]
        public IHttpActionResult DeleteListing(int id)
        {
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return NotFound();
            }

            db.Listings.Remove(listing);
            db.SaveChanges();

            return Ok(listing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListingExists(int id)
        {
            return db.Listings.Count(e => e.ListingId == id) > 0;
        }
    }
}