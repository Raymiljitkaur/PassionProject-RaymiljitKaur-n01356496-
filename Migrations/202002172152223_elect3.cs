namespace ElectronicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elect3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Electronics",
                c => new
                    {
                        ElectronicID = c.Int(nullable: false, identity: true),
                        ElectronicName = c.String(),
                        ElectronicType = c.String(),
                        ElectronicColor = c.String(),
                        ElectronicDescription = c.String(),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ElectronicID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .Index(t => t.BrandID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Electronics", "BrandID", "dbo.Brands");
            DropIndex("dbo.Electronics", new[] { "BrandID" });
            DropTable("dbo.Electronics");
            DropTable("dbo.Brands");
        }
    }
}
