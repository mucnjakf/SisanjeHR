namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Locations", "CityId", "dbo.Cities");
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Locations", new[] { "CityId" });
            RenameColumn(table: "dbo.Cities", name: "CountryId", newName: "Country_Id");
            RenameColumn(table: "dbo.Locations", name: "CityId", newName: "City_Id");
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int());
            AlterColumn("dbo.Locations", "City_Id", c => c.Int());
            CreateIndex("dbo.Cities", "Country_Id");
            CreateIndex("dbo.Locations", "City_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id");
            AddForeignKey("dbo.Locations", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Locations", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            AlterColumn("dbo.Locations", "City_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Locations", name: "City_Id", newName: "CityId");
            RenameColumn(table: "dbo.Cities", name: "Country_Id", newName: "CountryId");
            CreateIndex("dbo.Locations", "CityId");
            CreateIndex("dbo.Cities", "CountryId");
            AddForeignKey("dbo.Locations", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cities", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
