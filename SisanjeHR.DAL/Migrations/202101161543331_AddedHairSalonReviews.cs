namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHairSalonReviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        HairSalonId = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.HairSalonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Reviews", "HairSalonId", "dbo.HairSalons");
            DropIndex("dbo.Reviews", new[] { "HairSalonId" });
            DropIndex("dbo.Reviews", new[] { "RegisteredUserId" });
            DropTable("dbo.Reviews");
        }
    }
}
