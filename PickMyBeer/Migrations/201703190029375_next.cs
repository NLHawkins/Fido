namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Breweries", "BreweryDbId", c => c.String());
            AddColumn("dbo.Styles", "BreweryDbId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Styles", "BreweryDbId");
            DropColumn("dbo.Breweries", "BreweryDbId");
        }
    }
}
