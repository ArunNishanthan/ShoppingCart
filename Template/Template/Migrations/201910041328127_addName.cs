namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Name", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Name");
        }
    }
}
