namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityLocationsIdToCityAndLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "CityLocationsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "CityLocationsId");
        }
    }
}
