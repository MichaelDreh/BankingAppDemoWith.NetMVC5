namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedPhoneValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
        }
    }
}
