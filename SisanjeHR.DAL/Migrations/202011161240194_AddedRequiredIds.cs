namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            RenameColumn(table: "dbo.Cities", name: "Country_Id", newName: "CountryId");
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            AlterColumn("dbo.Cities", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "CountryId");
            AddForeignKey("dbo.Cities", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.RegisteredUsers", "LocationId", "dbo.Locations");
            DropIndex("dbo.RegisteredUsers", new[] { "LocationId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            AlterColumn("dbo.Cities", "CountryId", c => c.Int());
            DropTable("dbo.RegisteredUsers");
            RenameColumn(table: "dbo.Cities", name: "CountryId", newName: "Country_Id");
            CreateIndex("dbo.Cities", "Country_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
