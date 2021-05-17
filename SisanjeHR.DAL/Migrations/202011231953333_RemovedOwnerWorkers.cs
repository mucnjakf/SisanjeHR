namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedOwnerWorkers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workers", "Owner_Id", "dbo.Owners");
            DropIndex("dbo.Workers", new[] { "Owner_Id" });
            DropColumn("dbo.Workers", "Owner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workers", "Owner_Id", c => c.Int());
            CreateIndex("dbo.Workers", "Owner_Id");
            AddForeignKey("dbo.Workers", "Owner_Id", "dbo.Owners", "Id");
        }
    }
}
