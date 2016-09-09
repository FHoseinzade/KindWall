namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class response : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Responses", new[] { "Request_Id" });
            RenameColumn(table: "dbo.Responses", name: "Request_Id", newName: "RequestId");
            AlterColumn("dbo.Responses", "RequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responses", "RequestId");
            AddForeignKey("dbo.Responses", "RequestId", "dbo.Requests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "RequestId", "dbo.Requests");
            DropIndex("dbo.Responses", new[] { "RequestId" });
            AlterColumn("dbo.Responses", "RequestId", c => c.Int());
            RenameColumn(table: "dbo.Responses", name: "RequestId", newName: "Request_Id");
            CreateIndex("dbo.Responses", "Request_Id");
            AddForeignKey("dbo.Responses", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
