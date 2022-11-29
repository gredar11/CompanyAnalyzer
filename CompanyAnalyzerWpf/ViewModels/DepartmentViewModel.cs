using Domain.Models;
using Persistance;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class DepartmentViewModel:BindableBase
    {
        private readonly RepositoryManager _repositoryManager;
        public DepartmentViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        private Department _department;
        public Department Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        private DelegateCommand _loadEmployees;
        public DelegateCommand LoadEmployeesCommand =>
            _loadEmployees ?? (_loadEmployees = new DelegateCommand(ExecuteLoadEmployeesCommand));

        void ExecuteLoadEmployeesCommand()
        {
            var employees = _repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, true).Result;
            Employees.AddRange(employees);
        }
    }
}
