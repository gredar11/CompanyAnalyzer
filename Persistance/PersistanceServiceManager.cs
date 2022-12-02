using AutoMapper;
using Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class PersistanceServiceManager
    {
        public CompanyService CompanyService { get; set; }
        public DepartmentService DepartmentService { get; set; }
        public EmployeeService EmployeeService{ get; set; }
        public PersistanceServiceManager(RepositoryManager repositoryManager, IMapper mapper)
        {
            CompanyService = new CompanyService(repositoryManager, mapper);
            DepartmentService = new DepartmentService(repositoryManager, mapper);
            EmployeeService = new EmployeeService(repositoryManager, mapper);
        }
    }
}
