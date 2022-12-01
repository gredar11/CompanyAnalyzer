using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Persistance;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EditEmployeeDialogViewModel : BindableBase, IDialogAware
    {
        private readonly RepositoryManager _repositoryManager;

        public EditEmployeeDialogViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        EmployeeViewModel viewModel;
        public Employee Employee { get; set; }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set { SetProperty(ref _secondName, value); }
        }
        private DateOnly dateOfBirth;
        public DateOnly DateOfBirth
        {
            get { return dateOfBirth; }
            set { SetProperty(ref dateOfBirth, value); }
        }
        private DateOnly employmentDate;
        public DateOnly EmploymentDate
        {
            get { return employmentDate; }
            set { SetProperty(ref employmentDate, value); }
        }
        private string _post;
        public string Post
        {
            get { return _post; }
            set { SetProperty(ref _post, value); }
        }
        private double _salary;
        public double Salary
        {
            get { return _salary; }
            set { SetProperty(ref _salary, value); }
        }
        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();
        private Department _empDepartment;
        public Department EmpDepartment
        {
            get { return _empDepartment; }
            set
            {
                SetProperty(ref _empDepartment, value);
            }
        }
        private Company _company;
        public Company Company
        {
            get { return _company; }
            set { 
                SetProperty(ref _company, value);
                Departments.Clear();
                Departments.AddRange(_repositoryManager.DepartmentRepository.GetDepartments(Company.CompanyId, false).Result);
            }
        }
        public string Title => $"Editing {FirstName} {SecondName}";

        public event Action<IDialogResult> RequestClose;

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
            viewModel = (EmployeeViewModel)repuestObject;
            Employee = viewModel.Employee;
            FirstName = Employee.FirstName;
            SecondName = Employee.SecondName;
            Salary = Employee.Salary;
            Post = Employee.Post;
            DateOfBirth = Employee.DateOfBirth;
            EmploymentDate = Employee.EmploymentDate;
            EmpDepartment = Employee.Department;
            Company = Employee.Department.Company;
            Companies.AddRange(_repositoryManager.CompanyRepository.GetAll(false).Result);
        }
    }
}
