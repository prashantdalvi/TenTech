using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechApi.Data.Repository.IRepository;

namespace TechApi.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
        }
        public IEmployeeRepository Employee { get; private set; }

       
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
