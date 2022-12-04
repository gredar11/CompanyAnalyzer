using Contracts.cs.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyEmployeesDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }
        public void DeleteEmployee(Guid id)
        {
            var entity = RepositoryContext.Set<Employee>().Find(id);
            Delete(entity);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesByCompany(Guid companyId, Guid departmentId, bool trackChanges)
        {
            var company = await RepositoryContext.Set<Company>().Where(x => x.CompanyId == companyId).AsNoTracking().FirstOrDefaultAsync();
            if (company == null)
                return null;
            var departments = RepositoryContext.Set<Department>().Where(x => x.CompanyId == companyId).AsNoTracking().ToList();
            if (!departments.Any(x => x.DepartmentId == departmentId))
                return null;
            var res = await FindByCondition(x => x.DepartmentId == departmentId, trackChanges).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<Employee> GetEmployeeById(Guid companyId, Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var company = await RepositoryContext.Set<Company>().Where(x => x.CompanyId == companyId).Include(x => x.Departments).AsNoTracking().FirstOrDefaultAsync();
            if (company == null)
                return null;
            var departments = RepositoryContext.Set<Department>().Where(x => x.CompanyId == companyId).AsNoTracking().ToList();
            if (!departments.Any(x => x.DepartmentId == departmentId))
                return null;
            var res = await FindByCondition(x => x.DepartmentId == departmentId && x.EmployeeId == employeeId, trackChanges).SingleOrDefaultAsync();
            return res;
        }

        public async Task<Employee> GetEmployeeById(Guid employeeId, bool trackChanges)
        {

            var res = await FindByCondition(x => x.EmployeeId == employeeId, trackChanges).SingleOrDefaultAsync();
            return res;
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
