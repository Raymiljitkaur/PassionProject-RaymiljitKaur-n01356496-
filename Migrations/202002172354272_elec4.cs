namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elec4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderName = c.String(),
                        OrderPayType = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        OrderCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            AddColumn("dbo.Electronics", "Order_OrderID", c => c.Int());
            CreateIndex("dbo.Electronics", "Order_OrderID");
            AddForeignKey("dbo.Electronics", "Order_OrderID", "dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Electronics", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.Electronics", new[] { "Order_OrderID" });
            DropColumn("dbo.Electronics", "Order_OrderID");
            DropTable("dbo.Orders");
        }
    }
}
