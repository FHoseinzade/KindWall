namespace WallProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addResponse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestDetails", "Goods_Id", "dbo.Goods");
            DropForeignKey("dbo.RequestDetails", "Request_Id", "dbo.Requests");
            DropIndex("dbo.RequestDetails", new[] { "Goods_Id" });
            DropIndex("dbo.RequestDetails", new[] { "Request_Id" });
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reply = c.Boolean(nullable: false),
                        Request_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .Index(t => t.Request_Id);
            
            AddColumn("dbo.Requests", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "Goods_Id", c => c.Int());
            CreateIndex("dbo.Requests", "Goods_Id");
            AddForeignKey("dbo.Requests", "Goods_Id", "dbo.Goods", "Id");
            DropTable("dbo.RequestDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Goods_Id = c.Int(),
                        Request_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Responses", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "Goods_Id", "dbo.Goods");
            DropIndex("dbo.Responses", new[] { "Request_Id" });
            DropIndex("dbo.Requests", new[] { "Goods_Id" });
            DropColumn("dbo.Requests", "Goods_Id");
            DropColumn("dbo.Requests", "Count");
            DropTable("dbo.Responses");
            CreateIndex("dbo.RequestDetails", "Request_Id");
            CreateIndex("dbo.RequestDetails", "Goods_Id");
            AddForeignKey("dbo.RequestDetails", "Request_Id", "dbo.Requests", "Id");
            AddForeignKey("dbo.RequestDetails", "Goods_Id", "dbo.Goods", "Id");
        }
    }
}
