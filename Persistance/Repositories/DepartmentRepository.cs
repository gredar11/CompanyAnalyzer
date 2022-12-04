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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyEmployeesDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateDepartment(Department department)
        {
            Create(department);
        }

        public void DeleteDepartment(Guid id)
        {
            var entity = RepositoryContext.Set<Department>().Find(id);
            Delete(entity);
        }

        public async Task<Department> GetDepartmentById(Guid companyId, Guid departmentId, bool trackChanges)
        {
            var result = await FindByCondition(x => x.CompanyId == companyId && x.DepartmentId == departmentId, trackChanges).SingleOrDefaultAsync();
            return result;
        }

        public async Task<Department> GetDepartmentById(Guid departmentId, bool trackChanges)
        {
            var result = await FindByCondition(x => x.DepartmentId == departmentId, trackChanges).SingleOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Department>> GetDepartments(Guid companyId, bool trackChanges)
        {
            var departments = await FindByCondition(x => x.CompanyId == companyId, trackChanges).Include(x => x.Head).Include(x => x.Company).AsNoTracking().ToListAsync();
            return departments;
        }

        public void UpdateDepartment(Department department)
        {
            Update(department);
        }
    }
}
