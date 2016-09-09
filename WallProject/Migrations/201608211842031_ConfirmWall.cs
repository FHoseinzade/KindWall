namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfirmWall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Walls", "Confirm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Walls", "Confirm");
        }
    }
}
