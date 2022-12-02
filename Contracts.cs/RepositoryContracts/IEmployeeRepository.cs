using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.cs.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        Task DeleteEmployee(Guid id);
        Task<Employee> GetEmployeeById(Guid companyId, Guid departmentId, Guid employeeId, bool trackChanges);
        Task<Employee> GetEmployeeById(Guid employeeId, bool trackChanges);
        Task<IEnumerable<Employee>> GetAllEmployeesByCompany(Guid companyId, Guid departmentId, bool trackChanges);
        void UpdateEmployee(Employee employee);
        void CreateEmployee(Employee employee);
    }
}
