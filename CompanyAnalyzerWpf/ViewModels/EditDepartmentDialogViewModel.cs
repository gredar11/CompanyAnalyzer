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
        private Guid _headId;
        public Guid HeadId
        {
            get { return _headId; }
            set { SetProperty(ref _headId, value); }
        }
        private Employee _head;

        public event Action<IDialogResult> RequestClose;

        public Employee Head
        {
            get { return _head; }
            set { SetProperty(ref _head, value); }
        }
        public ObservableCollection<Employee> EmployeesOfDepartment { get; set; } = new ObservableCollection<Employee>();

        public string Title => $"Editing department {Department.DepartmentName}";

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
            EmployeesOfDepartment.AddRange(_repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, false).Result);
        }
    }
}
