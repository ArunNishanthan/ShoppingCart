namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ViewCarts", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ViewCarts", "imageUrl");
        }
    }
}
