namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractorSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillContractor",
                c => new
                    {
                        SkillId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillId, t.ContractorId })
                .ForeignKey("dbo.Skill", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Contractor", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.SkillId)
                .Index(t => t.ContractorId);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillContractor", "Contractor_ID", "dbo.Contractor");
            DropForeignKey("dbo.SkillContractor", "Skill_SkillId", "dbo.Skill");
            DropIndex("dbo.SkillContractor", new[] { "Contractor_ID" });
            DropIndex("dbo.SkillContractor", new[] { "Skill_SkillId" });
            DropTable("dbo.SkillContractor");
        }
    }
}
