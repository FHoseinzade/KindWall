namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeImageProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Picture", c => c.Binary());
            DropColumn("dbo.Goods", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Image", c => c.String());
            DropColumn("dbo.Goods", "Picture");
        }
    }
}
