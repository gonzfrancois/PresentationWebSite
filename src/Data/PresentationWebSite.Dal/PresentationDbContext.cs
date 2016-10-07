using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    public class PresentationDbContext : DbContext
    {
        public DbSet<Text> Texts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillCategory> SkillGategories { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }

        public PresentationDbContext() : base("PresentationWebSite")
        {
            Database.SetInitializer(new PresentationDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();
           //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
        }
    }
}
