using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechApi.Models;

namespace TechApi.Data.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee obj);
    }
}
