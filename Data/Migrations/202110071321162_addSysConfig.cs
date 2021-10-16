namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSysConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysConfigs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ValueString = c.String(),
                        ValueInt = c.Int(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SystemConfigs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Description = c.String(nullable: false),
                        ValueString = c.String(),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SysConfigs");
        }
    }
}
