namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexAddedOnCustomerEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Customers", "Email", unique: true);
            AddForeignKey("dbo.Admins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropIndex("dbo.Customers", new[] { "Email" });
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AddForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admins", "UserId", "dbo.Users", "Id");
        }
    }
}
