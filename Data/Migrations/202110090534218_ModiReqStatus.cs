namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModiReqStatus : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReqStatus", newName: "Status");
            DropIndex("dbo.Requests", new[] { "ReqStatus_Id" });
            DropColumn("dbo.Requests", "StatusId");
            RenameColumn(table: "dbo.Requests", name: "ReqStatus_Id", newName: "StatusId");
            AddColumn("dbo.Status", "Group", c => c.String());
            AddColumn("dbo.Status", "FilterExpression", c => c.String());
            AlterColumn("dbo.Requests", "StatusId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requests", "StatusId");
            DropColumn("dbo.Status", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "Description", c => c.String());
            DropIndex("dbo.Requests", new[] { "StatusId" });
            AlterColumn("dbo.Requests", "StatusId", c => c.String());
            DropColumn("dbo.Status", "FilterExpression");
            DropColumn("dbo.Status", "Group");
            RenameColumn(table: "dbo.Requests", name: "StatusId", newName: "ReqStatus_Id");
            AddColumn("dbo.Requests", "StatusId", c => c.String());
            CreateIndex("dbo.Requests", "ReqStatus_Id");
            RenameTable(name: "dbo.Status", newName: "ReqStatus");
        }
    }
}
