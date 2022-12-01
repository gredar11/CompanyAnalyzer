using CompanyAnalyzerWpf.Commands;
using CompanyAnalyzerWpf.Extensions;
using CompanyAnalyzerWpf.Tools;
using Persistance;
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
        private readonly RepositoryManager _repositoryManager;
        private readonly IDialogService _dialogService;
        public MainWindowViewModel(RepositoryManager repositoryManager, IDialogService dialogService)
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
                    return await _repositoryManager.CompanyRepository.GetAll(false);
                });
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
        private DelegateCommand<object> _showDialogCommand;
        public DelegateCommand<object> EditDialogCommand =>
            _showDialogCommand ?? (_showDialogCommand = new DelegateCommand<object>(ExecuteEditDialogCommand));

        void ExecuteEditDialogCommand(object selectedItem)
        {
            if (selectedItem is CompanyViewModel)
            {
                _dialogService.ShowDialog("EditCompanyDialog", new DialogParametersWithObj(SelectedItem), r =>
                {
                    if (r.Result == ButtonResult.OK)
                        RaisePropertyChanged(nameof(CompanyViewModel.CompanyName));
                });
            }
            else if (selectedItem is DepartmentViewModel)
            {
                _dialogService.ShowDialog("EditDepartmentDialog", new DialogParametersWithObj(SelectedItem), r =>
                {
                    if (r.Result == ButtonResult.OK)
                        RaisePropertyChanged(nameof(CompanyViewModel.CompanyName));
                });
            }
            else if (selectedItem is EmployeeViewModel)
            {
                _dialogService.ShowDialog("EditEmployeeDialog", new DialogParametersWithObj(SelectedItem), r =>
                {
                    if (r.Result == ButtonResult.OK)
                        RaisePropertyChanged(nameof(CompanyViewModel.CompanyName));
                });
            }
        }
        private DelegateCommand<object> _deleteDialogCommand;
        public DelegateCommand<object> DeleteDialogCommand =>
            _deleteDialogCommand ?? (_deleteDialogCommand = new DelegateCommand<object>(ExecuteDeleteDialogCommand));

        void ExecuteDeleteDialogCommand(object selectedItem)
        {
            _dialogService.ShowDialog("DeleteEntityDialog", new DialogParametersWithObj(SelectedItem), r =>
            {
                //if (r.Result == ButtonResult.OK)
                //    RaisePropertyChanged(nameof(CompanyViewModel.CompanyName));
            });
        }
        private DelegateCommand _showSalaryReport;
        public DelegateCommand ShowSalaryReportCommand =>
            _showSalaryReport ?? (_showSalaryReport = new DelegateCommand(ExecuteShowSalaryReportCommand));

        async void ExecuteShowSalaryReportCommand()
        {
            await _dialogService.ShowDialogAsync("SalaryReportView", new DialogParameters(""));
        }
        #endregion
    }
}
