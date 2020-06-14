namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ReceiverId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "ReceiverId");
        }
    }
}
