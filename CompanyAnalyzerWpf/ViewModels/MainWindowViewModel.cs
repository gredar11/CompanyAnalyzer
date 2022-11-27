using Contracts.cs.RepositoryContracts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly ICompanyRepository _companyRepository;
        public MainWindowViewModel(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        private DelegateCommand _loadCompanies;
        public DelegateCommand LoadCompaniesCommand =>
            _loadCompanies ?? (_loadCompanies = new DelegateCommand(ExecuteLoadCompaniesCommand));

        void ExecuteLoadCompaniesCommand()
        {
            var companies = _companyRepository.GetAll(false).Result;
            Console.WriteLine();
        }
    }
}
