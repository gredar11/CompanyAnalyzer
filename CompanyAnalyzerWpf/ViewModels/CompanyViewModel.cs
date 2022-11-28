using Contracts.cs.RepositoryContracts;
using Domain.Models;
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
        private readonly IDepartmentRepository _departmentRepository;

        public CompanyViewModel(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        private Company _company;
        public Company Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
        }
        private DelegateCommand _loadDepartments;
        public DelegateCommand LoadDepartmentsCommand =>
            _loadDepartments ?? (_loadDepartments = new DelegateCommand(ExecuteLoadDepartmentsCommand));

        void ExecuteLoadDepartmentsCommand()
        {
            var departments = _departmentRepository.GetDepartments(Company.CompanyId, false).Result;
            Departments.AddRange(departments);
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
