namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modiRequest2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SysConfigs", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.SysConfigs", "ValueInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SysConfigs", "ValueInt", c => c.Int());
            DropColumn("dbo.SysConfigs", "Year");
        }
    }
}
