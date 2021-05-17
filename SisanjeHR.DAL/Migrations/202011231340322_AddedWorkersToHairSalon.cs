namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWorkersToHairSalon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "HairSalonId", c => c.Int());
            CreateIndex("dbo.Workers", "HairSalonId");
            AddForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.Workers", new[] { "HairSalonId" });
            DropColumn("dbo.Workers", "HairSalonId");
        }
    }
}
