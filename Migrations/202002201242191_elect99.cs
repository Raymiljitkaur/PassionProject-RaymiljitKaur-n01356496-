namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elect99 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_CustomerID" });
            RenameColumn(table: "dbo.Orders", name: "Customer_CustomerID", newName: "CustomerID");
            AlterColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            AlterColumn("dbo.Orders", "CustomerID", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "CustomerID", newName: "Customer_CustomerID");
            CreateIndex("dbo.Orders", "Customer_CustomerID");
            AddForeignKey("dbo.Orders", "Customer_CustomerID", "dbo.Customers", "CustomerID");
        }
    }
}
