using CompanyAnalyzerWpf.Commands;
using Prism.Mvvm;
using Service;
using Service.Dtos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class CompanyViewModel : BindableBase
    {
        private readonly PersistanceServiceManager _repositoryManager;

        public CompanyViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            LoadDepartmentsCommand = new AsyncCommand(ExecuteLoadDepartmentsCommand, () => true);
        }
        public ObservableCollection<DepartmentViewModel> Departments { get; set; } = new ObservableCollection<DepartmentViewModel>();
        #region Properties
        public CompanyDto Company { get; set; }

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
            var departments = await Task.Run(() => _repositoryManager.DepartmentService.GetDepartments(Company.CompanyId, false));
            Departments.Clear();
            foreach (var item in departments)
            {
                Departments.Add(new DepartmentViewModel(_repositoryManager) { Department = item });
            }
        }
    }
}
