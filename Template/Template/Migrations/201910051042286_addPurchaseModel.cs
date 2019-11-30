namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPurchaseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchasedDate = c.DateTime(nullable: false),
                        ActivationCode = c.String(),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchasedProducts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.PurchasedProducts", new[] { "ProductId" });
            DropIndex("dbo.PurchasedProducts", new[] { "CustomerId" });
            DropTable("dbo.PurchasedProducts");
        }
    }
}
