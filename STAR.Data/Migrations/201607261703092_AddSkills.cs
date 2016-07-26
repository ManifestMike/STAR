namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.SkillId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Skill", new[] { "Name" });
            DropTable("dbo.Skill");
        }
    }
}
