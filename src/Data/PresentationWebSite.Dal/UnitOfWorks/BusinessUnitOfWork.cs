using System.Data.Entity;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.Repository.Base;
using PresentationWebSite.Dal.UnitOfWorks.Base;

namespace PresentationWebSite.Dal.UnitOfWorks
{
    public sealed class BusinessUnitOfWork : IBusinessUnitOfWork
    {
        private readonly PresentationDbContext _dbContext;
        private IGenericRepository<Text> _textsRepository;
        private IGenericRepository<Language> _languagesRepository;
        private IGenericRepository<Grade> _gradesRepository;
        private IGenericRepository<Skill> _skillsRepository;
        private IGenericRepository<SkillCategory> _skillCategoriesRepository;
        private IGenericRepository<Work> _worksRepository;
        private IGenericRepository<Job> _jobsRepository;
        private IGenericRepository<Hobby> _hobbiesRepository;
        private IGenericRepository<ApplicationUser> _usersRepository;

        public IGenericRepository<Text> TextsRepository
            => _textsRepository ?? (_textsRepository = new GenericRepository<Text>(_dbContext));
        public IGenericRepository<Language> LanguagesRepository
            => _languagesRepository ?? (_languagesRepository = new GenericRepository<Language>(_dbContext));
        public IGenericRepository<Grade> GradesRepository
            => _gradesRepository ?? (_gradesRepository = new GenericRepository<Grade>(_dbContext));
        public IGenericRepository<Skill> SkillsRepository
            => _skillsRepository ?? (_skillsRepository = new GenericRepository<Skill>(_dbContext));
        public IGenericRepository<SkillCategory> SkillCategoriesRepository
            => _skillCategoriesRepository ?? (_skillCategoriesRepository = new GenericRepository<SkillCategory>(_dbContext));
        public IGenericRepository<Work> WorksRepository
            => _worksRepository ?? (_worksRepository = new GenericRepository<Work>(_dbContext));
        public IGenericRepository<Job> JobsRepository
            => _jobsRepository ?? (_jobsRepository = new GenericRepository<Job>(_dbContext));
        public IGenericRepository<Hobby> HobbiesRepository
            => _hobbiesRepository ?? (_hobbiesRepository = new GenericRepository<Hobby>(_dbContext));
        public IGenericRepository<ApplicationUser> UsersRepository
            => _usersRepository ?? (_usersRepository = new GenericRepository<ApplicationUser>(_dbContext));

        public BusinessUnitOfWork() { }

        public BusinessUnitOfWork(string conString)
        {
            _dbContext = new PresentationDbContext(conString);
        }

        public void Dispose() => _dbContext.Dispose();
        public int Save() => _dbContext.SaveChanges();

        private DbContextTransaction _transaction;
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }
    }
}
