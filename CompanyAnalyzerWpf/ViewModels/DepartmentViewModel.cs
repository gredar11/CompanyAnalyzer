using CompanyAnalyzerWpf.Commands;
using Domain.Models;
using Persistance;
using Persistance.Dtos;
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
        private readonly PersistanceServiceManager _repositoryManager;
        private DepartmentDto department;

        public DepartmentViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            LoadEmployeesCommand = new AsyncCommand(ExecuteLoadEmployeesCommand, () => true);
        }
        public DepartmentDto Department
        {
            get => department; set
            {
                department = value;
                if (value.HeadEmployeeId.HasValue && value.CompanyId.HasValue)
                {
                    var head = _repositoryManager.EmployeeService.GetEmployeeById(value.CompanyId.Value, value.DepartmentId, value.HeadEmployeeId.Value, false).Result;
                    if (head is not null)
                        HeadName = head.FirstName + " " + head.SecondName;
                }
                if (value.CompanyId.HasValue)
                {
                    CompanyName = _repositoryManager.CompanyService.GetCompany(value.CompanyId.Value, false).Result.CompanyName;
                }
            }
        }
        public string CompanyName { get; set; }
        public string HeadName { get; set; }
        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new ObservableCollection<EmployeeViewModel>();
        public IAsyncCommand LoadEmployeesCommand { get; private set; }

        async Task ExecuteLoadEmployeesCommand()
        {
            Employees.Clear();
            var employees = await _repositoryManager.EmployeeService.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, true);

            foreach (var item in employees)
            {
                Employees.Add(new EmployeeViewModel() { Employee = item });
            }
        }
    }
}
