using CompanyAnalyzerWpf.Commands;
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
    public class DepartmentViewModel : BindableBase
    {
        private readonly RepositoryManager _repositoryManager;
        public DepartmentViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            LoadEmployeesCommand = new AsyncCommand(ExecuteLoadEmployeesCommand, () => true);
        }
        public Department Department { get; set; }
        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new ObservableCollection<EmployeeViewModel>();
        public IAsyncCommand LoadEmployeesCommand { get; private set; }

        async Task ExecuteLoadEmployeesCommand()
        {
            Employees.Clear();
            var employees = await _repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, true);
            foreach (var item in employees)
            {
                Employees.Add(new EmployeeViewModel() { Employee = item });
            }
        }
    }
}
