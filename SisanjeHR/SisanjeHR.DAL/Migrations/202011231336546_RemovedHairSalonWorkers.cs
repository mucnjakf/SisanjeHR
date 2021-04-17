namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedHairSalonWorkers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.Workers", new[] { "HairSalonId" });
            DropColumn("dbo.Workers", "HairSalonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workers", "HairSalonId", c => c.Int());
            CreateIndex("dbo.Workers", "HairSalonId");
            AddForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons", "Id");
        }
    }
}
