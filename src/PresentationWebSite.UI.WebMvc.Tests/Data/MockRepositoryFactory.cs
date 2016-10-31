using System.Collections.Generic;
using System.Linq;
using Moq;
using PresentationWebSite.Dal.Repository.Base;

namespace PresentationWebSite.UI.WebMvc.Tests.Data
{
    public static class MockRepositoryFactory<T> where T : class
    {
        public static Mock<IGenericRepository<T>> Create(List<T> entities)
        {
            var mock = new Mock<IGenericRepository<T>>();
            mock.Setup(x => x.Get())
                .Returns(entities);

            mock.Setup(x => x.Find(It.IsAny<T>()))
                .Returns((T t) => entities.FirstOrDefault(y => y == t));

            mock.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => entities.FirstOrDefault(y => ((dynamic)y).Id == (int)ids[0]));

            mock.Setup(x => x.Insert(It.IsAny<T>()))
                .Callback((T t) =>
                          {
                              ((dynamic) t).Id = entities.Count+1;
                              entities.Insert(entities.Count, t);
                          });

            mock.Setup(x => x.Delete(It.IsAny<T>()))
                .Callback((T t) => entities.Remove(t));

            return mock;
        }
    }
}
