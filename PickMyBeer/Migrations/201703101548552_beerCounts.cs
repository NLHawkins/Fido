namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beerCounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "BeerCount", c => c.Int(nullable: false));
            AddColumn("dbo.Patrons", "PrefBeerCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "PrefBeerCount");
            DropColumn("dbo.Bars", "BeerCount");
        }
    }
}
