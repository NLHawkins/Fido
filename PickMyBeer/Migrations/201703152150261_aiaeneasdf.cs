namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aiaeneasdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatronClientBeers", "PatronClient_Id", "dbo.PatronClients");
            DropForeignKey("dbo.PatronClientBeers", "Beer_Id", "dbo.Beers");
            DropIndex("dbo.PatronClientBeers", new[] { "PatronClient_Id" });
            DropIndex("dbo.PatronClientBeers", new[] { "Beer_Id" });
            CreateIndex("dbo.FaveBeers", "PatronClientId");
            CreateIndex("dbo.FaveBeers", "BeerId");
            AddForeignKey("dbo.FaveBeers", "BeerId", "dbo.Beers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FaveBeers", "PatronClientId", "dbo.PatronClients", "Id", cascadeDelete: true);
            DropTable("dbo.PatronClientBeers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PatronClientBeers",
                c => new
                    {
                        PatronClient_Id = c.Int(nullable: false),
                        Beer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatronClient_Id, t.Beer_Id });
            
            DropForeignKey("dbo.FaveBeers", "PatronClientId", "dbo.PatronClients");
            DropForeignKey("dbo.FaveBeers", "BeerId", "dbo.Beers");
            DropIndex("dbo.FaveBeers", new[] { "BeerId" });
            DropIndex("dbo.FaveBeers", new[] { "PatronClientId" });
            CreateIndex("dbo.PatronClientBeers", "Beer_Id");
            CreateIndex("dbo.PatronClientBeers", "PatronClient_Id");
            AddForeignKey("dbo.PatronClientBeers", "Beer_Id", "dbo.Beers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PatronClientBeers", "PatronClient_Id", "dbo.PatronClients", "Id", cascadeDelete: true);
        }
    }
}
