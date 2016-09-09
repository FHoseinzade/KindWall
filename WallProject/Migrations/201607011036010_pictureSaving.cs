namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pictureSaving : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "PictureName", c => c.String());
            DropColumn("dbo.Goods", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Picture", c => c.Binary());
            DropColumn("dbo.Goods", "PictureName");
        }
    }
}
