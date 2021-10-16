namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modiRequest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "RegisteredNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "RegisteredNo");
        }
    }
}
