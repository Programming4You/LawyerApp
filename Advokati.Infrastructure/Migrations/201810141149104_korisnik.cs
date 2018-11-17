namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class korisnik : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korisniks", "UlogaId", c => c.Int(nullable: false, defaultValue: 2));
            CreateIndex("dbo.Korisniks", "UlogaId");
            AddForeignKey("dbo.Korisniks", "UlogaId", "dbo.Ulogas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisniks", "UlogaId", "dbo.Ulogas");
            DropIndex("dbo.Korisniks", new[] { "UlogaId" });
            DropColumn("dbo.Korisniks", "UlogaId");
        }
    }
}
