namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beerinpkg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BeerArchives", "OnTap", c => c.Boolean(nullable: false));
            AddColumn("dbo.Beers", "BrewDbId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "BrewDbId");
            DropColumn("dbo.BeerArchives", "OnTap");
        }
    }
}
