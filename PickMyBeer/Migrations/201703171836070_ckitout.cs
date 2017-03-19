namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ckitout : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PrevPickedBeers", "PatronClientId");
            AddForeignKey("dbo.PrevPickedBeers", "PatronClientId", "dbo.PatronClients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrevPickedBeers", "PatronClientId", "dbo.PatronClients");
            DropIndex("dbo.PrevPickedBeers", new[] { "PatronClientId" });
        }
    }
}
