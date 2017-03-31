using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public string ListingName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactPhone { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Price { get; set; }
        public string ListingDescription { get; set; }
        public string ListingImage { get; set; }

        //foreign keys
        public virtual User User { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

    }
}