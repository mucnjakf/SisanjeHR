namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.CityLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HairSalonMethodsOfPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HairSalonId = c.Int(nullable: false),
                        MethodOfPaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.MethodOfPayments", t => t.MethodOfPaymentId, cascadeDelete: true)
                .Index(t => t.HairSalonId)
                .Index(t => t.MethodOfPaymentId);
            
            CreateTable(
                "dbo.HairSalons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Location_Id = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.HairSalonServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HairSalonId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.HairSalonId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HairSalonWorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HairSalonId = c.Int(nullable: false),
                        WorkingHourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.WorkingHours", t => t.WorkingHourId, cascadeDelete: true)
                .Index(t => t.HairSalonId)
                .Index(t => t.WorkingHourId);
            
            CreateTable(
                "dbo.WorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        HairSalon_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalon_Id)
                .Index(t => t.HairSalon_Id);
            
            CreateTable(
                "dbo.MethodOfPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Pin = c.String(),
                        Subscription_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscriptions", t => t.Subscription_Id)
                .Index(t => t.Subscription_Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Owners", "Subscription_Id", "dbo.Subscriptions");
            DropForeignKey("dbo.HairSalons", "Owner_Id", "dbo.Owners");
            DropForeignKey("dbo.HairSalonMethodsOfPayments", "MethodOfPaymentId", "dbo.MethodOfPayments");
            DropForeignKey("dbo.Workers", "HairSalon_Id", "dbo.HairSalons");
            DropForeignKey("dbo.HairSalons", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.HairSalonWorkingHours", "WorkingHourId", "dbo.WorkingHours");
            DropForeignKey("dbo.HairSalonWorkingHours", "HairSalonId", "dbo.HairSalons");
            DropForeignKey("dbo.HairSalonServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.HairSalonServices", "HairSalonId", "dbo.HairSalons");
            DropForeignKey("dbo.HairSalonMethodsOfPayments", "HairSalonId", "dbo.HairSalons");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.CityLocations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.CityLocations", "CityId", "dbo.Cities");
            DropIndex("dbo.Owners", new[] { "Subscription_Id" });
            DropIndex("dbo.Workers", new[] { "HairSalon_Id" });
            DropIndex("dbo.HairSalonWorkingHours", new[] { "WorkingHourId" });
            DropIndex("dbo.HairSalonWorkingHours", new[] { "HairSalonId" });
            DropIndex("dbo.HairSalonServices", new[] { "ServiceId" });
            DropIndex("dbo.HairSalonServices", new[] { "HairSalonId" });
            DropIndex("dbo.HairSalons", new[] { "Owner_Id" });
            DropIndex("dbo.HairSalons", new[] { "Location_Id" });
            DropIndex("dbo.HairSalonMethodsOfPayments", new[] { "MethodOfPaymentId" });
            DropIndex("dbo.HairSalonMethodsOfPayments", new[] { "HairSalonId" });
            DropIndex("dbo.CityLocations", new[] { "CityId" });
            DropIndex("dbo.CityLocations", new[] { "LocationId" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Owners");
            DropTable("dbo.MethodOfPayments");
            DropTable("dbo.Workers");
            DropTable("dbo.WorkingHours");
            DropTable("dbo.HairSalonWorkingHours");
            DropTable("dbo.Services");
            DropTable("dbo.HairSalonServices");
            DropTable("dbo.HairSalons");
            DropTable("dbo.HairSalonMethodsOfPayments");
            DropTable("dbo.Countries");
            DropTable("dbo.Locations");
            DropTable("dbo.CityLocations");
            DropTable("dbo.Cities");
        }
    }
}
