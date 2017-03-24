namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfawo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrevPickedBeers", "BarClientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrevPickedBeers", "BarClientId");
        }
    }
}
