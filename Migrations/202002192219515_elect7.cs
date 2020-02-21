namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elect7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerPhone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerPhone", c => c.String());
        }
    }
}
