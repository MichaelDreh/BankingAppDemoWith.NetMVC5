namespace BankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialPush : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        AccountNumber = c.Long(nullable: false),
                        BVN = c.Long(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PinHash = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        TransType = c.String(nullable: false),
                        Charges = c.Double(nullable: false),
                        Channel = c.String(nullable: false),
                        TransDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Roles");
            DropTable("dbo.Customers");
            DropTable("dbo.Users");
            DropTable("dbo.Admins");
        }
    }
}
