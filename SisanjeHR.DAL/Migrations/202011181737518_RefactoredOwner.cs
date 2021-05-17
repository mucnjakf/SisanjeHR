namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredOwner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Owners", "SubscriptionId", "dbo.Subscriptions");
            DropIndex("dbo.Owners", new[] { "SubscriptionId" });
            RenameColumn(table: "dbo.Owners", name: "SubscriptionId", newName: "Subscription_Id");
            AlterColumn("dbo.Owners", "Subscription_Id", c => c.Int());
            CreateIndex("dbo.Owners", "Subscription_Id");
            AddForeignKey("dbo.Owners", "Subscription_Id", "dbo.Subscriptions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Owners", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Owners", new[] { "Subscription_Id" });
            AlterColumn("dbo.Owners", "Subscription_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Owners", name: "Subscription_Id", newName: "SubscriptionId");
            CreateIndex("dbo.Owners", "SubscriptionId");
            AddForeignKey("dbo.Owners", "SubscriptionId", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
    }
}
