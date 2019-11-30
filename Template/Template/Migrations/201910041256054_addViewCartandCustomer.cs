namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addViewCartandCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ViewCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewCarts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ViewCarts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ViewCarts", new[] { "ProductId" });
            DropIndex("dbo.ViewCarts", new[] { "CustomerId" });
            DropTable("dbo.ViewCarts");
            DropTable("dbo.Customers");
        }
    }
}
