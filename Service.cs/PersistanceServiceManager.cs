using AutoMapper;
using Persistance;
using Persistance.Services;

namespace Service
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
