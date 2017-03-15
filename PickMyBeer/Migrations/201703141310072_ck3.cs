namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Breweries", "City", c => c.String());
            AddColumn("dbo.Breweries", "State", c => c.String());
            DropColumn("dbo.Breweries", "RegionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Breweries", "RegionId", c => c.String());
            DropColumn("dbo.Breweries", "State");
            DropColumn("dbo.Breweries", "City");
        }
    }
}
