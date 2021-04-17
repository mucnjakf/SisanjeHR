namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerHSTweak : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HairSalons", "Owner_Id", "dbo.Owners");
            DropIndex("dbo.HairSalons", new[] { "Owner_Id" });
            DropColumn("dbo.HairSalons", "Owner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HairSalons", "Owner_Id", c => c.Int());
            CreateIndex("dbo.HairSalons", "Owner_Id");
            AddForeignKey("dbo.HairSalons", "Owner_Id", "dbo.Owners", "Id");
        }
    }
}
