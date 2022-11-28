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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyEmployeesDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Department>> GetDepartments(Guid companyId, bool trackChanges)
        {
            var departments = await FindByCondition(x => x.CompanyId == companyId, trackChanges).ToListAsync();
            return departments;
        }
    }
}
