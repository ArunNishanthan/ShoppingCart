namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCustomer1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
    }
}
