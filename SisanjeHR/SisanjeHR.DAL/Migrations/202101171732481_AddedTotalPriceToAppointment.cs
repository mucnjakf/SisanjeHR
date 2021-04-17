namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalPriceToAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "TotalPrice");
        }
    }
}
