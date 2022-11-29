using CompanyAnalyzerWpf.Tools;
using Contracts.cs.RepositoryContracts;
using Domain.Models;
using Persistance;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IDialogService _dialogService;
        public MainWindowViewModel(RepositoryManager repositoryManager, IDialogService dialogService)
        {
            _repositoryManager = repositoryManager;
            _dialogService = dialogService;
        }
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        private DelegateCommand _loadCompanies;
        public DelegateCommand LoadCompaniesCommand =>
            _loadCompanies ?? (_loadCompanies = new DelegateCommand(ExecuteLoadCompaniesCommand));
        void ExecuteLoadCompaniesCommand()
        {
            var companies = _repositoryManager.CompanyRepository.GetAll(false).Result;
            foreach (var company in companies)
            {
                Companies.Add(new CompanyViewModel(_repositoryManager) { Company = company});
            }
            Console.WriteLine();
        }
        public ObservableCollection<CompanyViewModel> Companies { get; set; } = new ObservableCollection<CompanyViewModel>();

        private DelegateCommand<object> _showDialogCommand;
        public DelegateCommand<object> ShowDialogCommand =>
            _showDialogCommand ?? (_showDialogCommand = new DelegateCommand<object>(ExecuteShowDialogCommand));

        void ExecuteShowDialogCommand(object selectedItem)
        {
            _dialogService.ShowDialog("EditCompanyDialog", new DialogParametersWithObj(SelectedItem), r =>
            {
                
            });
        }
    }
}
