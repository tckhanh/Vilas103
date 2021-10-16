namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modiRequest : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Requests", new[] { "Company_Id" });
            DropColumn("dbo.Requests", "CompanyId");
            RenameColumn(table: "dbo.Requests", name: "Company_Id", newName: "CompanyId");
            AlterColumn("dbo.Requests", "CompanyId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requests", "CompanyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requests", new[] { "CompanyId" });
            AlterColumn("dbo.Requests", "CompanyId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Requests", name: "CompanyId", newName: "Company_Id");
            AddColumn("dbo.Requests", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "Company_Id");
        }
    }
}
