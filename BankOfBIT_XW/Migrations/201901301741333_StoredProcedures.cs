namespace BankOfBIT_XW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            this.Sql(BankOfBIT_XW_Properties.Resource.create_next_number);
        }
        
        public override void Down()
        {
            this.Sql(BankOfBIT_XW_Properties.Resource.drop_next_number);
        }
    }
}
