namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(maxLength: 256),
                        isFilled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Job");
        }
    }
}
