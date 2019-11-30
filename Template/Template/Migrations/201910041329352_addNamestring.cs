namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNamestring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Name", c => c.Int(nullable: false));
        }
    }
}
