namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColoum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "Sex", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "Sex", c => c.Int(nullable: false));
            AlterColumn("dbo.Student", "Name", c => c.String());
        }
    }
}
