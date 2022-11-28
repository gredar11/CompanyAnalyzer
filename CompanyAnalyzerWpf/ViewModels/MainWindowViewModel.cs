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

namespace CompanyAnalyzerWpf.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public MainWindowViewModel(ICompanyRepository companyRepository, IDepartmentRepository departmentRepository)
        {
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
        }

        private DelegateCommand _loadCompanies;
        public DelegateCommand LoadCompaniesCommand =>
            _loadCompanies ?? (_loadCompanies = new DelegateCommand(ExecuteLoadCompaniesCommand));

        
        void ExecuteLoadCompaniesCommand()
        {
            var companies = _companyRepository.GetAll(false).Result;
            foreach (var company in companies)
            {
                Companies.Add(new CompanyViewModel(_departmentRepository) { Company = company});
            }
            Console.WriteLine();
        }
        public ObservableCollection<CompanyViewModel> Companies { get; set; } = new ObservableCollection<CompanyViewModel>();
    }
}
