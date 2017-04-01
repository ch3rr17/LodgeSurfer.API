using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string ContactPhone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        

        //Collection
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual IEnumerable<Conversation> Conversations
        {
            get
            {
                return ConversationsOne.Concat(ConversationsTwo);
            }
        }
        public virtual ICollection<Conversation> ConversationsOne { get; set; }
        public virtual ICollection<Conversation> ConversationsTwo { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ListingSearch> ListingSearches { get; set; }

    }
}