namespace Transactions_notebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "Name", c => c.String());
            DropColumn("dbo.Quotes", "QuoteName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "QuoteName", c => c.String());
            DropColumn("dbo.Quotes", "Name");
        }
    }
}
