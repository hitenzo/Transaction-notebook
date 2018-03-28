namespace Transactions_notebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Quotes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Quotes", "Comment", c => c.String(nullable: false));
            AlterColumn("dbo.Quotes", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "Customer_Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            AlterColumn("dbo.Quotes", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Quotes", "Comment", c => c.String());
            AlterColumn("dbo.Quotes", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            CreateIndex("dbo.Quotes", "Customer_Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
