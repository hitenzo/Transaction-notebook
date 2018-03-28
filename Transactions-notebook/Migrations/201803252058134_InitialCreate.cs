namespace Transactions_notebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperationDate = c.DateTime(nullable: false),
                        QuoteName = c.String(),
                        Comment = c.String(),
                        TotalAmount = c.Double(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            DropTable("dbo.Quotes");
            DropTable("dbo.Customers");
        }
    }
}
