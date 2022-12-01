using Contracts.cs.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task DeleteEmployee(Guid companyId, Guid departmentId, Guid employeeId)
        {
            var employeeToDelete = await GetEmployeeById(companyId, departmentId, employeeId, trackChanges: true);
            Delete(employeeToDelete);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesByCompany(Guid companyId, Guid departmentId, bool trackChanges)
        {
            var company = await RepositoryContext.Set<Company>().Where(x => x.CompanyId == companyId).Include(x => x.Departments).AsNoTracking().FirstOrDefaultAsync();
            if (company == null)
                throw new Exception();
            if (!company.Departments.Any(x => x.DepartmentId == departmentId))
                throw new Exception();
            var res = await FindByCondition(x => x.DepartmentId == departmentId, trackChanges).Include(x => x.Department).ThenInclude(x => x.Company).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<Employee> GetEmployeeById(Guid companyId, Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var company = await RepositoryContext.Set<Company>().Where(x => x.CompanyId == companyId).Include(x => x.Departments).FirstOrDefaultAsync();
            if (company == null)
                throw new Exception();
            if (!company.Departments.Any(x => x.DepartmentId == departmentId))
                throw new Exception();
            var res = await FindByCondition(x => x.DepartmentId == departmentId && x.EmployeeId == employeeId, trackChanges).SingleOrDefaultAsync();
            return res;
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
