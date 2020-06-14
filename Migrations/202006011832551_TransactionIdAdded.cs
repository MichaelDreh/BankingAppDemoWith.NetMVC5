namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TransactionId");
        }
    }
}
