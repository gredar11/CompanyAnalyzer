using Domain.Models;
using Persistance.Dtos;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EmployeeViewModel : BindableBase
    {
        public EmployeeDto Employee { get; set; }
        
    }
}
