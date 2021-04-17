namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedManyToManyLocCity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CityLocations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.CityLocations", "LocationId", "dbo.Locations");
            DropIndex("dbo.CityLocations", new[] { "LocationId" });
            DropIndex("dbo.CityLocations", new[] { "CityId" });
            AddColumn("dbo.Locations", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Locations", "CityId");
            AddForeignKey("dbo.Locations", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            DropColumn("dbo.Cities", "CityLocationsId");
            DropColumn("dbo.Locations", "CityLocationsId");
            DropTable("dbo.CityLocations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CityLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Locations", "CityLocationsId", c => c.Int(nullable: false));
            AddColumn("dbo.Cities", "CityLocationsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Locations", "CityId", "dbo.Cities");
            DropIndex("dbo.Locations", new[] { "CityId" });
            DropColumn("dbo.Locations", "CityId");
            CreateIndex("dbo.CityLocations", "CityId");
            CreateIndex("dbo.CityLocations", "LocationId");
            AddForeignKey("dbo.CityLocations", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CityLocations", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
