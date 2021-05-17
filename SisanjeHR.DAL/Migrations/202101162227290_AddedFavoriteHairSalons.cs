namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteHairSalons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteHairSalons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HairSalonId = c.Int(nullable: false),
                        RegisteredUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.HairSalonId)
                .Index(t => t.RegisteredUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteHairSalons", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.FavoriteHairSalons", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.FavoriteHairSalons", new[] { "RegisteredUserId" });
            DropIndex("dbo.FavoriteHairSalons", new[] { "HairSalonId" });
            DropTable("dbo.FavoriteHairSalons");
        }
    }
}
