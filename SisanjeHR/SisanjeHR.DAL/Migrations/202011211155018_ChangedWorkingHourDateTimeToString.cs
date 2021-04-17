namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedWorkingHourDateTimeToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingHours", "DayInWeek", c => c.String());
            DropColumn("dbo.WorkingHours", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingHours", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkingHours", "DayInWeek");
        }
    }
}
