using LodgeSurfer.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Data
{
    public class LodgeSurferDataContext : DbContext
    {
        public LodgeSurferDataContext() : base("LodgeSurfer")
        {

        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Listing> Listings { get; set; }
        public IDbSet<ListingSearch> ListingSearches { get; set; }
        public IDbSet<Favorite> Favorites { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Conversation> Conversations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //A user has many favorites
            modelBuilder.Entity<User>()
                        .HasMany(f => f.Favorites)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);
            
            //A listing has many favorites
            modelBuilder.Entity<Listing>()
                        .HasMany(f => f.Favorites)
                        .WithRequired(l => l.Listing)
                        .HasForeignKey(l => l.ListingId)
                        .WillCascadeOnDelete(false);
            
            //A user has many listings
            modelBuilder.Entity<User>()
                        .HasMany(l => l.Listings)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            //A user has many listing searches
            modelBuilder.Entity<User>()
                        .HasMany(ls => ls.ListingSearches)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            //A user has many messages
            modelBuilder.Entity<User>()
                        .HasMany(m => m.Messages)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            //A conversation has many messages
            modelBuilder.Entity<Conversation>()
                        .HasMany(c => c.Messages)
                        .WithRequired(m => m.Conversation)
                        .HasForeignKey(m => m.ConversationId)
                        .WillCascadeOnDelete(false);

            //A conversation can have a sender(aka userone)
            modelBuilder.Entity<Conversation>()
                        .HasRequired(c => c.UserOne)
                        .WithMany(uone => uone.ConversationsOne)
                        .HasForeignKey(uone => uone.UserOneId);

            //A conversation can have a receiver(aka usertwo)
            modelBuilder.Entity<Conversation>()
                         .HasOptional(c => c.UserTwo)
                         .WithMany(utwo => utwo.ConversationsTwo)
                         .HasForeignKey(utwo => utwo.UserTwoId);
                        

            //compound key for favorite table
            modelBuilder.Entity<Favorite>()
                        .HasKey(f => new { f.UserId, f.ListingId });

        
            base.OnModelCreating(modelBuilder);
        }
    }
}