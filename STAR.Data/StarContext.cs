using STAR.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STAR.Data {
    public class StarContext : DbContext {
        public StarContext() : base("StarContext") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StarContext, Migrations.Configuration>("StarContext"));
        }

        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Contractor>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Contractor>().Property(c => c.LastName).IsRequired();

            modelBuilder.Entity<Position>().Property(s => s.Name)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Position>().Property(s => s.Description).HasMaxLength(256);

            modelBuilder.Entity<Skill>().Property(s => s.Name)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            modelBuilder.Entity<Skill>().Property(s => s.Description).HasMaxLength(256);

            modelBuilder.Entity<Skill>()
                .HasMany<Contractor>(s => s.Contractors)
                .WithMany(c => c.Skills)
                .Map(cs => {
                    cs.MapLeftKey("SkillId");
                    cs.MapRightKey("ContractorId");
                    cs.ToTable("SkillContractor");
                });
        }

        //public System.Data.Entity.DbSet<STAR.Web.Models.LoginModel> LoginModels { get; set; }

        // public System.Data.Entity.DbSet<STAR.Web.Models.LoginModel> LoginModels { get; set; }
    }
}
