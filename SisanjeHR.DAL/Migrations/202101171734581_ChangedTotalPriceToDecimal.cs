namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTotalPriceToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "TotalPrice", c => c.Double(nullable: false));
        }
    }
}
