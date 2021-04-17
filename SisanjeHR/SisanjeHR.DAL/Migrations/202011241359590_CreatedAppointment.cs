namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAppointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        IsCompleted = c.Boolean(nullable: false),
                        HairSalonId = c.Int(nullable: false),
                        RegisteredUserId = c.Int(nullable: false),
                        WorkerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HairSalons", t => t.HairSalonId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.HairSalonId)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.AppointmentServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.AppointmentId)
                .Index(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Appointments", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Appointments", "HairSalonId", "dbo.HairSalons");
            DropForeignKey("dbo.AppointmentServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.AppointmentServices", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.AppointmentServices", new[] { "ServiceId" });
            DropIndex("dbo.AppointmentServices", new[] { "AppointmentId" });
            DropIndex("dbo.Appointments", new[] { "WorkerId" });
            DropIndex("dbo.Appointments", new[] { "RegisteredUserId" });
            DropIndex("dbo.Appointments", new[] { "HairSalonId" });
            DropTable("dbo.AppointmentServices");
            DropTable("dbo.Appointments");
        }
    }
}
