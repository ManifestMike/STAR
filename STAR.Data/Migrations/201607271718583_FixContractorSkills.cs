namespace STAR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixContractorSkills : DbMigration
    {
        public override void Up()
        {
            /*RenameColumn(table: "dbo.SkillContractor", name: "Skill_SkillId", newName: "SkillId");
            RenameColumn(table: "dbo.SkillContractor", name: "Contractor_ID", newName: "ContractorId");
            RenameIndex(table: "dbo.SkillContractor", name: "IX_Skill_SkillId", newName: "IX_SkillId");
            RenameIndex(table: "dbo.SkillContractor", name: "IX_Contractor_ID", newName: "IX_ContractorId");
            */    
    }
        
        public override void Down()
        {
         /*   RenameIndex(table: "dbo.SkillContractor", name: "IX_ContractorId", newName: "IX_Contractor_ID");
            RenameIndex(table: "dbo.SkillContractor", name: "IX_SkillId", newName: "IX_Skill_SkillId");
            RenameColumn(table: "dbo.SkillContractor", name: "ContractorId", newName: "Contractor_ID");
            RenameColumn(table: "dbo.SkillContractor", name: "SkillId", newName: "Skill_SkillId");
        */    
        }
    }
}
