namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collectionsLabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "LabelURL", c => c.String());
            DropColumn("dbo.Bars", "BeerCount");
            DropColumn("dbo.Patrons", "PrefBeerCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patrons", "PrefBeerCount", c => c.Int(nullable: false));
            AddColumn("dbo.Bars", "BeerCount", c => c.Int(nullable: false));
            DropColumn("dbo.Beers", "LabelURL");
        }
    }
}
