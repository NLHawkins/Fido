namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfawfiw : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ImageUploads", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageUploads", "Timestamp", c => c.DateTime(nullable: false));
        }
    }
}
