namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOwnersWorkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Workers", "OwnerId");
            AddForeignKey("dbo.Workers", "OwnerId", "dbo.Owners", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Workers", new[] { "OwnerId" });
            DropColumn("dbo.Workers", "OwnerId");
        }
    }
}
