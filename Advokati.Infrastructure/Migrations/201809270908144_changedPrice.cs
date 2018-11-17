namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tasks", "Cena");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Cena", c => c.Int(nullable: false));
        }
    }
}
