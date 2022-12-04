using Prism.Mvvm;
using Service.Dtos;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EmployeeViewModel : BindableBase
    {
        public EmployeeDto Employee { get; set; }
        
    }
}
