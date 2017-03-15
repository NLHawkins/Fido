namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idtointonusers : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bars");
            DropPrimaryKey("dbo.Patrons");
            AlterColumn("dbo.Bars", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Patrons", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Bars", "Id");
            AddPrimaryKey("dbo.Patrons", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Patrons");
            DropPrimaryKey("dbo.Bars");
            AlterColumn("dbo.Patrons", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bars", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Patrons", "Id");
            AddPrimaryKey("dbo.Bars", "Id");
        }
    }
}
