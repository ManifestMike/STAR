namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contractor");
        }
    }
}
