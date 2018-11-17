namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Cena", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "Placeno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Placeno");
            DropColumn("dbo.Tasks", "Cena");
        }
    }
}
