using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.cs.RepositoryContracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments(Guid companyId, bool trackChanges);
        Task<Department> GetDepartmentById(Guid companyId, Guid departmentId, bool trackChanges);
        Task DeleteDepartment(Guid companyId, Guid departmentId);
        void UpdateDepartment(Department department);
        void CreateDepartment(Department department);
    }
}
