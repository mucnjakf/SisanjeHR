namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeStartAndEnd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingHours", "TimeStart", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.WorkingHours", "TimeEnd", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.WorkingHours", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingHours", "Time", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.WorkingHours", "TimeEnd");
            DropColumn("dbo.WorkingHours", "TimeStart");
        }
    }
}
