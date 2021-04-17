namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkerHairSalonIsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.Workers", new[] { "HairSalonId" });
            AlterColumn("dbo.Workers", "HairSalonId", c => c.Int());
            CreateIndex("dbo.Workers", "HairSalonId");
            AddForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.Workers", new[] { "HairSalonId" });
            AlterColumn("dbo.Workers", "HairSalonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Workers", "HairSalonId");
            AddForeignKey("dbo.Workers", "HairSalonId", "dbo.HairSalons", "Id", cascadeDelete: true);
        }
    }
}
