namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecoveryCodeLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecoverPasswords", "RecoveryCode", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecoverPasswords", "RecoveryCode", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
