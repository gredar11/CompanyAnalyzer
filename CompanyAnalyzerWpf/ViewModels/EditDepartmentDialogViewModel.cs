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
using System.Linq;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EditDepartmentDialogViewModel : BindableBase, IDialogAware
    {
        private bool newEntity = false;
        private readonly PersistanceServiceManager _repositoryManager;
        public EditDepartmentDialogViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public DepartmentDto Department { get; set; }
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { SetProperty(ref _departmentName, value); }
        }
        private CompanyDto _company;
        public CompanyDto Company
        {
            get { return _company; }
            set
            {
                SetProperty(ref _company, value);
                EmployeesOfDepartment.Clear();
                if (value is not null && !newEntity)
                {
                    var res = _repositoryManager.EmployeeService.GetAllEmployeesByCompany(value.CompanyId, Department.DepartmentId, false).Result;
                    EmployeesOfDepartment.AddRange(res);
                }
            }
        }
        public ObservableCollection<CompanyDto> Companies { get; set; } = new ObservableCollection<CompanyDto>();
        public event Action<IDialogResult> RequestClose;

        private EmployeeDto _head;
        public EmployeeDto Head
        {
            get { return _head; }
            set { SetProperty(ref _head, value); }
        }
        public ObservableCollection<EmployeeDto> EmployeesOfDepartment { get; set; } = new ObservableCollection<EmployeeDto>();

        public string Title => $"Editing department {Department.DepartmentName}";
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                Department.HeadEmployeeId = Head?.EmployeeId;
                Department.DepartmentName = DepartmentName;
                Department.CompanyId = Company.CompanyId;
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
            Department = (DepartmentDto)requestObject.RequestParameter;
            DepartmentName = Department.DepartmentName;
            if (!requestObject.CreateNew)
            {
                if (Department.HeadEmployeeId.HasValue)
                    Head = _repositoryManager.EmployeeService.GetEmployeeById(Department.CompanyId.Value, Department.DepartmentId, Department.HeadEmployeeId.Value, false).Result;
                Company = _repositoryManager.CompanyService.GetCompany(Department.CompanyId.Value, false).Result;
            }
            Companies.AddRange(_repositoryManager.CompanyService.GetAll(false).Result);
            if (Company is not null)
            {
                EmployeesOfDepartment.AddRange(_repositoryManager.EmployeeService.GetAllEmployeesByCompany(Department.CompanyId.Value, Department.DepartmentId, false).Result);
                return;
            }
            newEntity = true;
        }
    }
}
