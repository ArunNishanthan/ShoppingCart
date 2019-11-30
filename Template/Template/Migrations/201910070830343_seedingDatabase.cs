namespace Template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Photoshop','Editing and compositing for photos, web and mobile app designs, 3D artwork, videos, and more.','/Content/image/1_Photoshop.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Stock','Access millions of high-quality creative assets inside your favorite Creative Cloud apps.','/Content/image/2_Stock.svg',36.88)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Lightroom Classic','Desktop-focused photo editing.','/Content/image/4_LightroomClassic.svg',22.80)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Illustrator','Create beautiful vector art and illustrations.','/Content/image/5_Illustrator.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('InDesign','Craft elegant layouts at your desk or on the go.','/Content/image/6_Indesign.svg',9.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Adobe XD','Design, prototype, and share user experiences.','/Content/image/7_AdobeXD.svg',9.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Adobe Premiere Rush','Create and share online videos anywhere.','/Content/image/8_AdobePremiereRush.svg',9.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Adobe Fresco','Freeform drawing and painting for Apple Pencil and touch devices.','/Content/image/9_AdobeFresco.svg',9.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Adobe Premiere Pro','Edit media in its native format and create productions for film, TV, and web.','/Content/image/10_AdobePremierePro.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('After Effects','Create motion graphics and visual effects for film, TV, video, and web.','/Content/image/11_AfterEffects.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Dimension','Create photorealistic 3D images for branding, product shots, and package design.','/Content/image/12_Dimension.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Dreamweaver','Design and develop modern, responsive websites.','/Content/image/13_DreamWeaver.svg',20.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Acrobat Pro DC','The complete PDF solution for working anywhere. (includes desktop, web, and mobile access)','/Content/image/14_AdobeAcrobatPro.png',14.99)");
            Sql("INSERT INTO Products (Name, ShortDescription,ImageUrl, Price) VALUES('Acrobat Pro 2017','The complete desktop solution for working with PDFs. (one-time purchase)','/Content/image/15_AdobeAcrobat2017.png',13.99)");
        }
        
        public override void Down()
        {
        }
    }
}
