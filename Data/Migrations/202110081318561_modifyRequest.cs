namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Opinions", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Opinions", new[] { "Request_Id" });
            DropColumn("dbo.Opinions", "RequestID");
            RenameColumn(table: "dbo.Opinions", name: "Request_Id", newName: "RequestID");
            DropPrimaryKey("dbo.Requests");
            AddColumn("dbo.Requests", "ReceivedNo", c => c.String());
            AlterColumn("dbo.Requests", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Opinions", "RequestID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Requests", "Id");
            CreateIndex("dbo.Opinions", "RequestID");
            AddForeignKey("dbo.Opinions", "RequestID", "dbo.Requests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "RequestID", "dbo.Requests");
            DropIndex("dbo.Opinions", new[] { "RequestID" });
            DropPrimaryKey("dbo.Requests");
            AlterColumn("dbo.Opinions", "RequestID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Requests", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Requests", "ReceivedNo");
            AddPrimaryKey("dbo.Requests", "Id");
            RenameColumn(table: "dbo.Opinions", name: "RequestID", newName: "Request_Id");
            AddColumn("dbo.Opinions", "RequestID", c => c.Int(nullable: false));
            CreateIndex("dbo.Opinions", "Request_Id");
            AddForeignKey("dbo.Opinions", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
