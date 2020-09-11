namespace BankOfBIT_XW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientNumber = c.Long(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 35),
                        LastName = c.String(nullable: false, maxLength: 35),
                        Address = c.String(nullable: false, maxLength: 300),
                        City = c.String(nullable: false, maxLength: 300),
                        Province = c.String(nullable: false, maxLength: 2),
                        PostalCode = c.String(nullable: false, maxLength: 7),
                        DateCreated = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.AccountStates",
                c => new
                    {
                        AccountStateId = c.Int(nullable: false, identity: true),
                        LowerLimit = c.Double(nullable: false),
                        UpperLimit = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountStateId);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Long(nullable: false),
                        ClientId = c.Int(nullable: false),
                        AccountStateId = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                        OpeningBalance = c.Double(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Notes = c.String(),
                        SavingServiceCharges = c.Double(),
                        InterestRate = c.Double(),
                        MortgageRate = c.Double(),
                        Amortization = c.Int(),
                        ChequingServiceCharges = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.AccountStates", t => t.AccountStateId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.AccountStateId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BankAccounts", new[] { "ClientId" });
            DropIndex("dbo.BankAccounts", new[] { "AccountStateId" });
            DropForeignKey("dbo.BankAccounts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.BankAccounts", "AccountStateId", "dbo.AccountStates");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AccountStates");
            DropTable("dbo.Clients");
        }
    }
}
