namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elct5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Electronics", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.Electronics", new[] { "Order_OrderID" });
            CreateTable(
                "dbo.OrderElectronics",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Electronic_ElectronicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Electronic_ElectronicID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Electronics", t => t.Electronic_ElectronicID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Electronic_ElectronicID);
            
            DropColumn("dbo.Electronics", "Order_OrderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Electronics", "Order_OrderID", c => c.Int());
            DropForeignKey("dbo.OrderElectronics", "Electronic_ElectronicID", "dbo.Electronics");
            DropForeignKey("dbo.OrderElectronics", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderElectronics", new[] { "Electronic_ElectronicID" });
            DropIndex("dbo.OrderElectronics", new[] { "Order_OrderID" });
            DropTable("dbo.OrderElectronics");
            CreateIndex("dbo.Electronics", "Order_OrderID");
            AddForeignKey("dbo.Electronics", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
