using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechApi.Data.Repository.IRepository;
using TechApi.Models;

namespace TechApi.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Employee obj)
        {
            _db.Employee.Update(obj);
        }
    }
}
