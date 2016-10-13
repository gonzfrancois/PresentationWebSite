using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.Repository.Base;

namespace PresentationWebSite.Dal.UnitOfWorks.Base
{
    public interface IBusinessUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Text> TextsRepository { get; }
        IGenericRepository<Language> LanguagesRepository { get; }
        IGenericRepository<Grade> GradesRepository { get; }
        IGenericRepository<Skill> SkillsRepository { get; }
        IGenericRepository<SkillCategory> SkillCategoriesRepository { get; }
        IGenericRepository<Work> WorksRepository { get; }
        IGenericRepository<Job> JobsRepository { get; }
        IGenericRepository<Hobby> HobbiesRepository { get; }
        IGenericRepository<ApplicationUser> UsersRepository { get; }
    }
}
