namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfinawfoiw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrefBeerId = c.Int(nullable: false),
                        MatchBeerId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beers", t => t.MatchBeerId, cascadeDelete: false)
                .ForeignKey("dbo.Beers", t => t.PrefBeerId, cascadeDelete: false)
                .Index(t => t.PrefBeerId)
                .Index(t => t.MatchBeerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "PrefBeerId", "dbo.Beers");
            DropForeignKey("dbo.Matches", "MatchBeerId", "dbo.Beers");
            DropIndex("dbo.Matches", new[] { "MatchBeerId" });
            DropIndex("dbo.Matches", new[] { "PrefBeerId" });
            DropTable("dbo.Matches");
        }
    }
}
