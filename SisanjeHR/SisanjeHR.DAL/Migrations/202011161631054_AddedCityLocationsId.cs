namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityLocationsId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "CityLocationsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "CityLocationsId");
        }
    }
}
