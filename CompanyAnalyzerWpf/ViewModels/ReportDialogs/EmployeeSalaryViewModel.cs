using Prism.Mvvm;
using Service.Dtos;

namespace CompanyAnalyzerWpf.ViewModels.ReportDialogs
{
    public class EmployeeSalaryViewModel : BindableBase
    {
        public EmployeeSalaryViewModel(EmployeeDto employee, string companyName, string departmentName)
        {
            Salary = employee.Salary;
            FullName = employee.FirstName + " " + employee.SecondName;
            CompanyName = companyName;
            DepartmentName = departmentName;
        }
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { SetProperty(ref _departmentName, value); }
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }
        private double _salary;
        public double Salary
        {
            get { return _salary; }
            set { SetProperty(ref _salary, value); }
        }
    }
}
