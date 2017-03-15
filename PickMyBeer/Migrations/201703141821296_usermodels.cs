namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bars", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Patrons", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bars", new[] { "UserId" });
            DropIndex("dbo.Patrons", new[] { "UserId" });
            AddColumn("dbo.Bars", "UserName", c => c.String());
            AlterColumn("dbo.Bars", "UserId", c => c.String());
            AlterColumn("dbo.Patrons", "UserId", c => c.String());
            DropColumn("dbo.Bars", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bars", "Name", c => c.String());
            AlterColumn("dbo.Patrons", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Bars", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Bars", "UserName");
            CreateIndex("dbo.Patrons", "UserId");
            CreateIndex("dbo.Bars", "UserId");
            AddForeignKey("dbo.Patrons", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bars", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
