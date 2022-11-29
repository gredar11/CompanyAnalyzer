using Contracts.cs.RepositoryContracts;
using Domain.Models;
using Persistance;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class CompanyViewModel : BindableBase
    {
        private readonly RepositoryManager _repositoryManager;

        public CompanyViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public ObservableCollection<DepartmentViewModel> Departments { get; set; } = new ObservableCollection<DepartmentViewModel>();
        private Company _company;
        public Company Company
        {
            get { return _company; }
            set
            {
                SetProperty(ref _company, value);
                CompanyName = value.CompanyName;
                EstablishmentDate = value.EstablishmentDate;
                Adress = value.Adress;
            }
        }
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                SetProperty(ref _companyName, value);
                RaisePropertyChanged(nameof(Company));
            }
        }
        private DateOnly _establishmentDate;
        public DateOnly EstablishmentDate
        {
            get { return _establishmentDate; }
            set
            {
                SetProperty(ref _establishmentDate, value);
                RaisePropertyChanged(nameof(Company));
            }
        }
        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set
            {
                SetProperty(ref _adress, value);
                RaisePropertyChanged(nameof(Company));
            }
        }
        private DelegateCommand _loadDepartments;
        public DelegateCommand LoadDepartmentsCommand =>
            _loadDepartments ?? (_loadDepartments = new DelegateCommand(ExecuteLoadDepartmentsCommand));

        void ExecuteLoadDepartmentsCommand()
        {
            var departments = _repositoryManager.DepartmentRepository.GetDepartments(Company.CompanyId, false).Result;
            foreach (var item in departments)
            {
                Departments.Add(new DepartmentViewModel(_repositoryManager) { Department = item });
            }
        }
        private DelegateCommand _updateCompany;
        public DelegateCommand UpdateCompanyCommand =>
            _updateCompany ?? (_updateCompany = new DelegateCommand(ExecuteUpdateCompanyCommand));

        void ExecuteUpdateCompanyCommand()
        {
            RaisePropertyChanged(nameof(Company));
        }
    }
}
