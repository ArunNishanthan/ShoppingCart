namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FullName", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Userid", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "ConfirmPassword", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Password", c => c.String());
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "ConfirmPassword");
            DropColumn("dbo.Customers", "Userid");
            DropColumn("dbo.Customers", "FullName");
        }
    }
}
