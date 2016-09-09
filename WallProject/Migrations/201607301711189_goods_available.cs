namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goods_available : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Available", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "Available");
        }
    }
}
