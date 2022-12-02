﻿using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Persistance;
using Persistance.Dtos;
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
        private readonly PersistanceServiceManager _repositoryManager;

        public EditEmployeeDialogViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public EmployeeDto Employee { get; set; }
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
        public ObservableCollection<DepartmentDto> Departments { get; set; } = new ObservableCollection<DepartmentDto>();
        public ObservableCollection<CompanyDto> Companies { get; set; } = new ObservableCollection<CompanyDto>();
        private DepartmentDto _empDepartment;
        public DepartmentDto EmpDepartment
        {
            get { return _empDepartment; }
            set
            {
                SetProperty(ref _empDepartment, value);
            }
        }
        private CompanyDto _company;
        public CompanyDto Company
        {
            get { return _company; }
            set
            {
                SetProperty(ref _company, value);
                Departments.Clear();
                if (Company is not null)
                    Departments.AddRange(_repositoryManager.DepartmentService.GetDepartments(Company.CompanyId, false).Result);
            }
        }
        public string Title => $"Editing {FirstName} {SecondName}";

        public event Action<IDialogResult> RequestClose;
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                Employee.FirstName = FirstName;
                Employee.SecondName = SecondName;
                Employee.Salary = Salary;
                Employee.Post = Post;
                Employee.DateOfBirth = DateOfBirth;
                Employee.EmploymentDate = EmploymentDate;
                Employee.DepartmentId = EmpDepartment.DepartmentId;
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
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));


        public void OnDialogOpened(IDialogParameters parameters)
        {
            var requestObject = (parameters as DialogParametersWithObj);
            Employee = (EmployeeDto)requestObject.RequestParameter;
            FirstName = Employee.FirstName;
            SecondName = Employee.SecondName;
            Salary = Employee.Salary;
            Post = Employee.Post;
            DateOfBirth = Employee.DateOfBirth;
            EmploymentDate = Employee.EmploymentDate;
            if (!requestObject.CreateNew)
            {
                EmpDepartment = _repositoryManager.DepartmentService.GetDepartmentById(Employee.DepartmentId.Value, false).Result;
                Company = _repositoryManager.CompanyService.GetCompany(EmpDepartment.CompanyId.Value, false).Result;
            }
            Companies.AddRange(_repositoryManager.CompanyService.GetAll(false).Result);
            
        }
    }
}
