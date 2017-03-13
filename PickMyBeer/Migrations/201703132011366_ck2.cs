namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "UserId", c => c.String());
            AddColumn("dbo.Patrons", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "UserId");
            DropColumn("dbo.Bars", "UserId");
        }
    }
}
