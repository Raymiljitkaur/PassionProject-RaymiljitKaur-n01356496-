namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elect8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerAddress", c => c.String());
            DropColumn("dbo.Customers", "CustomerPhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerPhone", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "CustomerAddress");
        }
    }
}
