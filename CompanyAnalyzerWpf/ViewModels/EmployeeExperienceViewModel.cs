using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EmployeeExperienceViewModel:BindableBase
    {
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }
        private string _employeeFullName;
        public string EmployeeFullName
        {
            get { return _employeeFullName; }
            set { SetProperty(ref _employeeFullName, value); }
        }
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { SetProperty(ref _departmentName, value); }
        }
        private int _experience;
        public int Experience
        {
            get { return _experience; }
            set { SetProperty(ref _experience, value); }
        }
    }
}
