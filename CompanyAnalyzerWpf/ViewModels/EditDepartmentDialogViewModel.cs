using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Persistance;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EditDepartmentDialogViewModel : BindableBase, IDialogAware
    {
        DepartmentViewModel viewModel;
        private readonly RepositoryManager _repositoryManager;
        public EditDepartmentDialogViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public Department Department { get; set; }
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { SetProperty(ref _departmentName, value); }
        }
        private Company _company;
        public Company Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value);
                EmployeesOfDepartment.Clear();
                EmployeesOfDepartment.AddRange(_repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(Company.CompanyId, Department.DepartmentId, false).Result);
            }
        }
        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();
        public event Action<IDialogResult> RequestClose;

        private Employee _head;
        public Employee Head
        {
            get { return _head; }
            set { SetProperty(ref _head, value); }
        }
        public ObservableCollection<Employee> EmployeesOfDepartment { get; set; } = new ObservableCollection<Employee>();

        public string Title => $"Editing department {Department.DepartmentName}";
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                viewModel.Department.Head = Head;
                viewModel.Department.HeadEmployeeId = Head.EmployeeId;
                viewModel.Department.DepartmentName = DepartmentName;
                viewModel.Department.Company = Company;
                viewModel.Department.CompanyId = Company.CompanyId;
                _repositoryManager.DepartmentRepository.UpdateDepartment(viewModel.Department);
                _repositoryManager.SaveAsync();
                result = ButtonResult.OK;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var repuestObject = (parameters as DialogParametersWithObj).RequestParameter;
            viewModel = (DepartmentViewModel)repuestObject;
            Department= viewModel.Department;
            Head = Department.Head;
            DepartmentName = Department.DepartmentName;
            Companies.AddRange(_repositoryManager.CompanyRepository.GetAll(false).Result);
            EmployeesOfDepartment.AddRange(_repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, false).Result);
        }
    }
}
