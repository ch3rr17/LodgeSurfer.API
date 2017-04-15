using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class Favorite
    {
        //scalar properties
        public int UserId { get; set; }
        public int ListingId { get; set; }

        //Navigation properties
        public virtual User User { get; set; }
        public virtual Listing Listing { get; set; }
    }
}