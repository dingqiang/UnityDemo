namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        IsDel = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        DeleteTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Student");
        }
    }
}
