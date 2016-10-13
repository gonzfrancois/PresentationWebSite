using System.Data.Entity;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    public interface IPresentationDbContext
    {
        IDbSet<Grade> Grades { get; set; }
        IDbSet<Hobby> Hobbies { get; set; }
        IDbSet<Job> Jobs { get; set; }
        IDbSet<Language> Languages { get; set; }
        IDbSet<SkillCategory> SkillGategories { get; set; }
        IDbSet<Skill> Skills { get; set; }
        IDbSet<Text> Texts { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<Work> Works { get; set; }

        int SaveChanges();
    }
}