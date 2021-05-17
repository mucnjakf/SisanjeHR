namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerHSTweak2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HairSalons", "OwnerId", c => c.Int(nullable: true));
            CreateIndex("dbo.HairSalons", "OwnerId");
            AddForeignKey("dbo.HairSalons", "OwnerId", "dbo.Owners", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HairSalons", "OwnerId", "dbo.Owners");
            DropIndex("dbo.HairSalons", new[] { "OwnerId" });
            DropColumn("dbo.HairSalons", "OwnerId");
        }
    }
}
