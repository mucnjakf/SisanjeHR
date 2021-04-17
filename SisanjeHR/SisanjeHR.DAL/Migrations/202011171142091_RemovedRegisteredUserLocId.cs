namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRegisteredUserLocId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "LocationId", "dbo.Locations");
            DropIndex("dbo.RegisteredUsers", new[] { "LocationId" });
            RenameColumn(table: "dbo.RegisteredUsers", name: "LocationId", newName: "Location_Id");
            AlterColumn("dbo.RegisteredUsers", "Location_Id", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "Location_Id");
            AddForeignKey("dbo.RegisteredUsers", "Location_Id", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "Location_Id", "dbo.Locations");
            DropIndex("dbo.RegisteredUsers", new[] { "Location_Id" });
            AlterColumn("dbo.RegisteredUsers", "Location_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.RegisteredUsers", name: "Location_Id", newName: "LocationId");
            CreateIndex("dbo.RegisteredUsers", "LocationId");
            AddForeignKey("dbo.RegisteredUsers", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
