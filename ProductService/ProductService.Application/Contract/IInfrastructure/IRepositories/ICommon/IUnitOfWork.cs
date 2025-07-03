using ProductService.Application.Contract.IInfrastructure.IRepositories.IEventStoreRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> GetRepository<T>() where T : class;
        IEventStoreRepository EventStoreRepository { get; }
        Task SaveChangesAsync();
    }
}
