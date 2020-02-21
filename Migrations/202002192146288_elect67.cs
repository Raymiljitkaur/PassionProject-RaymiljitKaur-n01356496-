namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elect67 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerPhone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            AddColumn("dbo.Orders", "Customer_CustomerID", c => c.Int());
            CreateIndex("dbo.Orders", "Customer_CustomerID");
            AddForeignKey("dbo.Orders", "Customer_CustomerID", "dbo.Customers", "CustomerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_CustomerID" });
            DropColumn("dbo.Orders", "Customer_CustomerID");
            DropTable("dbo.Customers");
        }
    }
}
