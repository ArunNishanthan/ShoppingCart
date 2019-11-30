namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addShortDes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShortDescription");
        }
    }
}
