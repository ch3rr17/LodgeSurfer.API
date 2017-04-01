namespace LodgeSurfer.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.Int(nullable: false, identity: true),
                        UserOneId = c.Int(nullable: false),
                        UserTwoId = c.Int(),
                    })
                .PrimaryKey(t => t.ConversationId)
                .ForeignKey("dbo.Users", t => t.UserOneId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserTwoId)
                .Index(t => t.UserOneId)
                .Index(t => t.UserTwoId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        ConversationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MessageText = c.String(),
                        MessageTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.ConversationId)
                .Index(t => t.ConversationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ListingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ListingId })
                .ForeignKey("dbo.Listings", t => t.ListingId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ListingId);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListingId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ListingName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        ContactPhone = c.String(),
                        Bedroom = c.Int(nullable: false),
                        Bathroom = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        ListingDescription = c.String(),
                        ListingImage = c.String(),
                    })
                .PrimaryKey(t => t.ListingId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ListingSearches",
                c => new
                    {
                        ListingSearchId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        City = c.String(),
                        ZipCode = c.String(),
                        MinimumPrice = c.Int(),
                        MaximumPrice = c.Int(),
                        Bedroom = c.Int(),
                        Bathroom = c.Int(),
                    })
                .PrimaryKey(t => t.ListingSearchId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "UserTwoId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "UserOneId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.ListingSearches", "UserId", "dbo.Users");
            DropForeignKey("dbo.Listings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "ListingId", "dbo.Listings");
            DropIndex("dbo.ListingSearches", new[] { "UserId" });
            DropIndex("dbo.Listings", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "ListingId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "UserTwoId" });
            DropIndex("dbo.Conversations", new[] { "UserOneId" });
            DropTable("dbo.ListingSearches");
            DropTable("dbo.Listings");
            DropTable("dbo.Favorites");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}
