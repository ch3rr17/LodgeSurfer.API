using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class ListingSearch
    {
        public int ListingSearchId { get; set; }

        [Required]
        public int UserId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public int? MinimumPrice { get; set; }
        public int? MaximumPrice { get; set; }
        public int? Bedroom { get; set; }
        public int? Bathroom { get; set; }

        public virtual User User { get; set; }

    }
}