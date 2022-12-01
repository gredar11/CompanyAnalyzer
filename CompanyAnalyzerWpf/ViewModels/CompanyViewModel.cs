using CompanyAnalyzerWpf.Commands;
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
            LoadDepartmentsCommand = new AsyncCommand(ExecuteLoadDepartmentsCommand, () => true);
        }
        public ObservableCollection<DepartmentViewModel> Departments { get; set; } = new ObservableCollection<DepartmentViewModel>();
        #region Properties
        public Company Company { get; set; }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }
        #endregion
        public IAsyncCommand LoadDepartmentsCommand { get; private set; }

        async Task ExecuteLoadDepartmentsCommand()
        {
            var departments = await _repositoryManager.DepartmentRepository.GetDepartments(Company.CompanyId, false);
            Departments.Clear();
            foreach (var item in departments)
            {
                Departments.Add(new DepartmentViewModel(_repositoryManager) { Department = item });
            }
        }
    }
}
