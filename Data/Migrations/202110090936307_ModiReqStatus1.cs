namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModiReqStatus1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "Description", c => c.String());
            DropColumn("dbo.Status", "FilterExpression");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "FilterExpression", c => c.String());
            DropColumn("dbo.Status", "Description");
        }
    }
}
