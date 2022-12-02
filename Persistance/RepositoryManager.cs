using Contracts.cs.RepositoryContracts;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class RepositoryManager : IDisposable
    {
        private readonly CompanyEmployeesDbContext _repositoryConext;
        public RepositoryManager(CompanyEmployeesDbContext repositoryConext)
        {
            CompanyRepository = new CompanyRepository(repositoryConext);
            DepartmentRepository = new DepartmentRepository(repositoryConext);
            EmployeeRepository = new EmployeeRepository(repositoryConext);
            _repositoryConext = repositoryConext;
        }

        public ICompanyRepository CompanyRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }

        public void Dispose()
        {
            _repositoryConext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _repositoryConext.SaveChangesAsync();
        }
    }
}
