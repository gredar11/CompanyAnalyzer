using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EmployeeExperience
    {
        public string CompanyName { get; set; }

        public string EmployeeFullName { get; set; }

        public string DepartmentName { get; set; }

        public int Experience { get; set; }
        public int Age { get; set; }
        public int BirthAge { get; set; }
    }
}
