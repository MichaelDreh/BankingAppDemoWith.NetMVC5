namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiverNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ReceiverName", c => c.String());
            DropColumn("dbo.Transactions", "ReceiverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "ReceiverId", c => c.Int(nullable: false));
            DropColumn("dbo.Transactions", "ReceiverName");
        }
    }
}
