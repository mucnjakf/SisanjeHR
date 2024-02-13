﻿namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectedOwnerHairSalon2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HairSalons", "Owner_Id", c => c.Int());
            CreateIndex("dbo.HairSalons", "Owner_Id");
            AddForeignKey("dbo.HairSalons", "Owner_Id", "dbo.Owners", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HairSalons", "Owner_Id", "dbo.Owners");
            DropIndex("dbo.HairSalons", new[] { "Owner_Id" });
            DropColumn("dbo.HairSalons", "Owner_Id");
        }
    }
}
