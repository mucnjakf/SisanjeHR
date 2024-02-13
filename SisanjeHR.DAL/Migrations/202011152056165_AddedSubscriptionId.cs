namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubscriptionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Owners", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Owners", new[] { "Subscription_Id" });
            RenameColumn(table: "dbo.Owners", name: "Subscription_Id", newName: "SubscriptionId");
            AlterColumn("dbo.Owners", "SubscriptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Owners", "SubscriptionId");
            AddForeignKey("dbo.Owners", "SubscriptionId", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Owners", "SubscriptionId", "dbo.Subscriptions");
            DropIndex("dbo.Owners", new[] { "SubscriptionId" });
            AlterColumn("dbo.Owners", "SubscriptionId", c => c.Int());
            RenameColumn(table: "dbo.Owners", name: "SubscriptionId", newName: "Subscription_Id");
            CreateIndex("dbo.Owners", "Subscription_Id");
            AddForeignKey("dbo.Owners", "Subscription_Id", "dbo.Subscriptions", "Id");
        }
    }
}
