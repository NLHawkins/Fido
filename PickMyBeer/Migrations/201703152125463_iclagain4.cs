namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iclagain4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Beers", "PatronClient_Id", "dbo.PatronClients");
            DropIndex("dbo.Beers", new[] { "PatronClient_Id" });
            CreateTable(
                "dbo.PatronClientBeers",
                c => new
                    {
                        PatronClient_Id = c.Int(nullable: false),
                        Beer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatronClient_Id, t.Beer_Id })
                .ForeignKey("dbo.PatronClients", t => t.PatronClient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Beers", t => t.Beer_Id, cascadeDelete: true)
                .Index(t => t.PatronClient_Id)
                .Index(t => t.Beer_Id);
            
            DropColumn("dbo.Beers", "PatronClient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Beers", "PatronClient_Id", c => c.Int());
            DropForeignKey("dbo.PatronClientBeers", "Beer_Id", "dbo.Beers");
            DropForeignKey("dbo.PatronClientBeers", "PatronClient_Id", "dbo.PatronClients");
            DropIndex("dbo.PatronClientBeers", new[] { "Beer_Id" });
            DropIndex("dbo.PatronClientBeers", new[] { "PatronClient_Id" });
            DropTable("dbo.PatronClientBeers");
            CreateIndex("dbo.Beers", "PatronClient_Id");
            AddForeignKey("dbo.Beers", "PatronClient_Id", "dbo.PatronClients", "Id");
        }
    }
}
