namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttemptToRename : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(maxLength: 256),
                        isFilled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId);
            
            DropTable("dbo.Job");
        }
        
        public override void Down()
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
            
            DropTable("dbo.Position");
        }
    }
}
