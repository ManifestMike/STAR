namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColumntoContractorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Position", "contractorId", c => c.Int(nullable: true));
            DropColumn("dbo.Position", "isFilled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Position", "isFilled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Position", "contractorId");
        }
    }
}
