using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechApi.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
