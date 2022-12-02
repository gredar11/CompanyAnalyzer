using CompanyAnalyzerWpf.Commands;
using CompanyAnalyzerWpf.Extensions;
using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Persistance;
using Persistance.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set => SetProperty(ref _isLoading, value);
        }
        private bool _dataIsLoaded = false;
        private readonly PersistanceServiceManager _repositoryManager;
        private readonly IDialogService _dialogService;
        public MainWindowViewModel(PersistanceServiceManager repositoryManager, IDialogService dialogService)
        {
            _repositoryManager = repositoryManager;
            _dialogService = dialogService;
            LoadCompaniesAsync = new AsyncCommand(ExecuteLoadCompaniesAsync, CanExecuteLoadCompanies);
        }
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public IAsyncCommand LoadCompaniesAsync { get; private set; }
        private async Task ExecuteLoadCompaniesAsync()
        {
            try
            {
                IsLoading = true;
                var companies = await Task.Run(async () =>
                {
                    return await _repositoryManager.CompanyService.GetAll(false);
                });
                Companies.Clear();
                foreach (var company in companies)
                {
                    Companies.Add(new CompanyViewModel(_repositoryManager) { Company = company });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
                _dataIsLoaded = true;
            }
        }
        private bool CanExecuteLoadCompanies()
        {
            return !_dataIsLoaded;
        }

        public ObservableCollection<CompanyViewModel> Companies { get; set; } = new ObservableCollection<CompanyViewModel>();

        #region DialogCommands
        private DelegateCommand<object> _editCreateCommand;
        public DelegateCommand<object> EditCreateDialogCommand =>
            _editCreateCommand ?? (_editCreateCommand = new DelegateCommand<object>(ExecuteEditCreateDialogCommand));

        void ExecuteEditCreateDialogCommand(object parameter)
        {
            if (parameter is Type)
            {
                var type = (Type)parameter;
                object entityInstance = Activator.CreateInstance(type);
                ShowDialogCreate(entityInstance);
                return;
            }
            ShowDialogWindowEdit();
        }
        private async void ShowDialogCreate(object itemToCreateOrEdit)
        {
            switch (itemToCreateOrEdit)
            {
                case CompanyDto company:
                    _dialogService.ShowDialog("EditCompanyDialog", new DialogParametersWithObj(itemToCreateOrEdit, true), async r =>
                    {
                        // обновить коллекции
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.CompanyService.CreateCompany(company);
                            await ExecuteLoadCompaniesAsync();
                        }
                    });
                    break;
                case DepartmentDto department:
                    _dialogService.ShowDialog("EditDepartmentDialog", new DialogParametersWithObj(itemToCreateOrEdit, true), async r =>
                    {
                        // обновить коллекции
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.DepartmentService.CreateDepartment(department);
                            await ExecuteLoadCompaniesAsync();

                        }
                    });
                    break;
                case EmployeeDto employee:
                    _dialogService.ShowDialog("EditEmployeeDialog", new DialogParametersWithObj(itemToCreateOrEdit, true), async r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.EmployeeService.CreateEmployee(employee);
                            await ExecuteLoadCompaniesAsync();
                        }
                    });
                    break;
            }
        }
        private async void ShowDialogWindowEdit()
        {
            switch (SelectedItem)
            {
                case CompanyViewModel companyViewModel:
                    _dialogService.ShowDialog("EditCompanyDialog", new DialogParametersWithObj(companyViewModel.Company, false), async r =>
                    {
                        // обновить коллекции
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.CompanyService.UpdateCompany(companyViewModel.Company);
                            await ExecuteLoadCompaniesAsync();
                        }
                    });
                    break;
                case DepartmentViewModel departmentViewModel:
                    _dialogService.ShowDialog("EditDepartmentDialog", new DialogParametersWithObj(departmentViewModel.Department, false), async r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.DepartmentService.UpdateDepartment(departmentViewModel.Department);
                            await ExecuteLoadCompaniesAsync();
                        }
                    });
                    break;
                case EmployeeViewModel employeeViewModel:
                    _dialogService.ShowDialog("EditEmployeeDialog", new DialogParametersWithObj(employeeViewModel.Employee, false), async r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            _repositoryManager.EmployeeService.UpdateEmployee(employeeViewModel.Employee);
                            await ExecuteLoadCompaniesAsync();
                        }
                    });
                    break;
            }
        }

        private DelegateCommand<object> _deleteDialogCommand;
        public DelegateCommand<object> DeleteDialogCommand =>
            _deleteDialogCommand ?? (_deleteDialogCommand = new DelegateCommand<object>(ExecuteDeleteDialogCommand));

        async void ExecuteDeleteDialogCommand(object selectedItem)
        {
            _dialogService.ShowDialog("DeleteEntityDialog", new DialogParametersWithObj(selectedItem, false), async r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    await ExecuteLoadCompaniesAsync();
                }
            });
        }
        private DelegateCommand _showSalaryReport;
        public DelegateCommand ShowSalaryReportCommand =>
            _showSalaryReport ?? (_showSalaryReport = new DelegateCommand(ExecuteShowSalaryReportCommand));

        async void ExecuteShowSalaryReportCommand()
        {
            await _dialogService.ShowDialogAsync("SalaryReportView", new DialogParameters(""));
        }
        private DelegateCommand _showExperienceReport;
        public DelegateCommand ShowExperienceReportCommand =>
            _showExperienceReport ?? (_showExperienceReport = new DelegateCommand(ExecuteShowExperienceReport));

        async void ExecuteShowExperienceReport()
        {
            await _dialogService.ShowDialogAsync("ExperienceReportView", new DialogParameters(""));
        }
        #endregion
    }
}
