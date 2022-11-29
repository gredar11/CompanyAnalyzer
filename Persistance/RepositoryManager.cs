using Contracts.cs.RepositoryContracts;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class RepositoryManager
    {
        private readonly CompanyEmployeesDbContext _repositoryConext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public RepositoryManager(CompanyEmployeesDbContext repositoryConext)
        {
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryConext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(repositoryConext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryConext));
            _repositoryConext = repositoryConext;
        }

        public ICompanyRepository CompanyRepository => _companyRepository.Value;
        public IDepartmentRepository DepartmentRepository=> _departmentRepository.Value;
        public IEmployeeRepository EmployeeRepository=> _employeeRepository.Value;
        public async Task SaveAsync()
        {
            await _repositoryConext.SaveChangesAsync();
        }
    }
}
