using System;

namespace PresentationWebSite.Dal.UnitOfWorks.Base
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        
        //void BeginTransaction();

        //void Commit();

        //void Rollback();

    }
}
