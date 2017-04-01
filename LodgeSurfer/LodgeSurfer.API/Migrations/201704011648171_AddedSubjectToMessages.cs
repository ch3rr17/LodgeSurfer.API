namespace LodgeSurfer.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubjectToMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Subject");
        }
    }
}
