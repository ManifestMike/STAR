using STAR.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace STAR.Data {
    public class StarContext : DbContext {
        public StarContext() : base("StarContext") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StarContext, Migrations.Configuration>("StarContext"));
        }

        public virtual DbSet<Contractor> Contractors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Contractor>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Contractor>().Property(c => c.LastName).IsRequired();
        }
    }
}
