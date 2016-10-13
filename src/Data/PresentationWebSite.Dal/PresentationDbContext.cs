using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    public class PresentationDbContext : DbContext, IPresentationDbContext
    {
        public IDbSet<Text> Texts { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<Grade> Grades { get; set; }
        public IDbSet<Skill> Skills { get; set; }
        public IDbSet<SkillCategory> SkillGategories { get; set; }
        public IDbSet<Work> Works { get; set; }
        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<Hobby> Hobbies { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }

        public PresentationDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new PresentationDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
