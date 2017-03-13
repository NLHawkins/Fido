namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRolesFK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bars", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Patrons", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bars", "UserId");
            CreateIndex("dbo.Patrons", "UserId");
            AddForeignKey("dbo.Bars", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Patrons", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patrons", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bars", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Patrons", new[] { "UserId" });
            DropIndex("dbo.Bars", new[] { "UserId" });
            AlterColumn("dbo.Patrons", "UserId", c => c.String());
            AlterColumn("dbo.Bars", "UserId", c => c.String());
        }
    }
}
