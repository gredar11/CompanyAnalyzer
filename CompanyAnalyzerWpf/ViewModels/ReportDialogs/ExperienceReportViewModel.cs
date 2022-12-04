using CompanyAnalyzerWpf.Tools;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels.ReportDialogs
{
    public class ExperienceReportViewModel : BindableBase, IDialogAware
    {
        private readonly PersistanceServiceManager _repositoryManager;

        public ExperienceReportViewModel(PersistanceServiceManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public ObservableCollection<EmployeeExperience> FilteredEmployees { get; set; } = new ObservableCollection<EmployeeExperience>();
        public ObservableCollection<EmployeeExperience> AllEmployees { get; set; } = new ObservableCollection<EmployeeExperience>();
        private string _selectedCompanyName;
        public string SelectedCompanyName
        {
            get { return _selectedCompanyName; }
            set
            {
                SetProperty(ref _selectedCompanyName, value);
                var filteredData = AllEmployees.Where(x => x.CompanyName == value).ToList();
                FilteredEmployees.Clear();
                FilteredEmployees.AddRange(filteredData);
            }
        }
        public ObservableCollection<string> CompanyNames { get; set; } = new ObservableCollection<string>();
        private int _selectedExperience;
        public int SelectedExperience
        {
            get { return _selectedExperience; }
            set
            {
                SetProperty(ref _selectedExperience, value);
                var filteredData = AllEmployees.Where(x => x.Experience == value && x.CompanyName == SelectedCompanyName).ToList();
                FilteredEmployees.Clear();
                FilteredEmployees.AddRange(filteredData);
            }
        }
        public ObservableCollection<int> ExperienceYears { get; set; } = new ObservableCollection<int>();
        private Options _ageOrYearFilerSelectedItem;
        public Options AgeOrYearFilerSelectedItem
        {
            get { return _ageOrYearFilerSelectedItem; }
            set
            {
                SetProperty(ref _ageOrYearFilerSelectedItem, value);
                YearsToFilter.Clear();
                if (value == Options.YearOfBirth)
                {
                    var values = FilteredEmployees.Select(x => x.BirthAge).ToList();
                    YearsToFilter.AddRange(values);
                }
                if (value == Options.Age)
                {
                    var values = FilteredEmployees.Select(x => x.Age).ToList();
                    YearsToFilter.AddRange(values);

                }
            }
        }
        public ObservableCollection<Options> FilterOptions { get; set; } = new ObservableCollection<Options>() { Options.Age, Options.YearOfBirth };
        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                SetProperty(ref _selectedYear, value);
                var filteredData = AllEmployees.Where(x => x.Experience == value && x.CompanyName == SelectedCompanyName && x.Experience == SelectedExperience).ToList();
                FilteredEmployees.Clear();
                FilteredEmployees.AddRange(filteredData);
            }
        }
        public ObservableCollection<int> YearsToFilter { get; set; } = new ObservableCollection<int>();

        public string Title => "Experience report";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }


        public void OnDialogClosed()
        {
        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            await Task.Run(() => LoadExperiencesAsync());
        }

        private async Task LoadExperiencesAsync()
        {
            var allCompanyEntities = await _repositoryManager.CompanyService.GetAll(false);
            foreach (var company in allCompanyEntities)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => CompanyNames.Add(company.CompanyName));
                var departments = await _repositoryManager.DepartmentService.GetDepartments(company.CompanyId, false);
                foreach (var dep in departments)
                {
                    var employees = await _repositoryManager.EmployeeService.GetAllEmployeesByCompany(company.CompanyId, dep.DepartmentId, false);
                    foreach (var emp in employees)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {

                            AllEmployees.Add(new EmployeeExperience()
                            {
                                Age = DateTime.Now.Year - emp.DateOfBirth.Year,
                                BirthAge = emp.DateOfBirth.Year,
                                Experience = DateTime.Now.Year - emp.EmploymentDate.Year,
                                CompanyName = company.CompanyName,
                                EmployeeFullName = emp.FirstName + " " + emp.SecondName,
                                DepartmentName = dep.DepartmentName
                            });
                            ExperienceYears.Add(DateTime.Now.Year - emp.EmploymentDate.Year);
                        });

                    }
                }
            }
            System.Windows.Application.Current.Dispatcher.Invoke(() => FilteredEmployees.AddRange(AllEmployees));
        }
    }
}

