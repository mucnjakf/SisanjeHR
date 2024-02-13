namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Appointments", "IsDeclined", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "IsDeclined");
            DropColumn("dbo.Appointments", "IsAccepted");
        }
    }
}
