namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMethodOfPaymentToAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "MethodOfPaymentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "MethodOfPaymentId");
            AddForeignKey("dbo.Appointments", "MethodOfPaymentId", "dbo.MethodOfPayments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "MethodOfPaymentId", "dbo.MethodOfPayments");
            DropIndex("dbo.Appointments", new[] { "MethodOfPaymentId" });
            DropColumn("dbo.Appointments", "MethodOfPaymentId");
        }
    }
}
