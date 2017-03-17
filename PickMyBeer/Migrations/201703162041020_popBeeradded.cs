namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class popBeeradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PopBeers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .Index(t => t.BeerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PopBeers", "BeerId", "dbo.Beers");
            DropIndex("dbo.PopBeers", new[] { "BeerId" });
            DropTable("dbo.PopBeers");
        }
    }
}
