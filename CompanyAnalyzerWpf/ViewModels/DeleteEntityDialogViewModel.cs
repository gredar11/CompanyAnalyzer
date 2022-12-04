using CompanyAnalyzerWpf.Tools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Service;
using System;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class DeleteEntityDialogViewModel : BindableBase, IDialogAware
    {
        private readonly PersistanceServiceManager _repositoryManager;
        public DeleteEntityDialogViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        private object Entity { get; set; }

        public string Title => $"Delete entity [{GetEntityInfo()}]";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));
        public void OnDialogClosed()
        {

        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Entity = (parameters as DialogParametersWithObj).RequestParameter;
            switch (Entity)
            {
                case CompanyViewModel companyViewModel:
                    RequestClose = DeleteCompany;
                    break;
                case EmployeeViewModel employeeViewModel:
                    RequestClose = DeleteEmployee;
                    break;
                case DepartmentViewModel departmentViewModel:
                    RequestClose = DeleteDepartment;
                    break;
                default:
                    break;
            }

        }
        private string GetEntityInfo()
        {
            switch (Entity)
            {
                case CompanyViewModel companyViewModel:
                    return companyViewModel.Company.CompanyName;
                case DepartmentViewModel departmentViewModel:
                    return departmentViewModel.Department.DepartmentName;
                case EmployeeViewModel employeeViewModel:
                    return employeeViewModel.Employee.FirstName + " " + employeeViewModel.Employee.SecondName;
                default:
                    break;
            }
            return string.Empty;
        }
        private void DeleteCompany(IDialogResult result)
        {
            if (result.Result == ButtonResult.OK)
            {
                _repositoryManager.CompanyService.DeleteCompany((Entity as CompanyViewModel).Company.CompanyId);
            }
        }
        private void DeleteDepartment(IDialogResult result)
        {
            if (result.Result == ButtonResult.OK)
            {
                var dep = (Entity as DepartmentViewModel).Department!;
                _repositoryManager.DepartmentService.DeleteDepartment(dep.DepartmentId);
            }
        }
        private void DeleteEmployee(IDialogResult result)
        {
            if (result.Result == ButtonResult.OK)
            {
                var employee = (Entity as EmployeeViewModel).Employee!;
                _repositoryManager.EmployeeService.DeleteEmployee(employee.EmployeeId);
            }
        }
    }
}
