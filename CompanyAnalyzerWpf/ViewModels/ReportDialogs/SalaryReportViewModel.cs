using Prism.Mvvm;
using Prism.Services.Dialogs;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels.ReportDialogs
{
    public class SalaryReportViewModel : BindableBase, IDialogAware
    {
        private readonly PersistanceServiceManager _repositoryManager;
        public SalaryReportViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;

        }
        public ObservableCollection<EmployeeSalaryViewModel> Employees { get; set; } = new ObservableCollection<EmployeeSalaryViewModel>();

        public string Title => "Salary Report";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            List<EmployeeSalaryViewModel> models = new List<EmployeeSalaryViewModel>();
            await Task.Run(async () =>
            {
                var companies = await _repositoryManager.CompanyService.GetAll(false);
                foreach (var company in companies)
                {
                    var companyDepartments = await _repositoryManager.DepartmentService.GetDepartments(company.CompanyId, false);
                    foreach (var department in companyDepartments)
                    {
                        var employees = await _repositoryManager.EmployeeService.GetAllEmployeesByCompany(company.CompanyId, department.DepartmentId, false);
                        foreach (var employee in employees)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                models.Add(new EmployeeSalaryViewModel(employee, company.CompanyName, department.DepartmentName));
                            });
                        }
                    }
                }
            });
            Employees.AddRange(models);
        }
    }
}
