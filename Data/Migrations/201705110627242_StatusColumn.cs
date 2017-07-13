namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "Status");
        }
    }
}
