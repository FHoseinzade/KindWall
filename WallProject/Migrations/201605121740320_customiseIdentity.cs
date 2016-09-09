namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customiseIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Family", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Family");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
