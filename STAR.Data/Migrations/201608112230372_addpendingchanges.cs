namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpendingchanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Position", "contractorId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Position", "contractorId", c => c.Int(nullable: false));
        }
    }
}
