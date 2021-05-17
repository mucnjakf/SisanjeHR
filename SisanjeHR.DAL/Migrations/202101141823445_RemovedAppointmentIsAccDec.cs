namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAppointmentIsAccDec : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "IsAccepted");
            DropColumn("dbo.Appointments", "IsDeclined");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "IsDeclined", c => c.Boolean(nullable: false));
            AddColumn("dbo.Appointments", "IsAccepted", c => c.Boolean(nullable: false));
        }
    }
}
