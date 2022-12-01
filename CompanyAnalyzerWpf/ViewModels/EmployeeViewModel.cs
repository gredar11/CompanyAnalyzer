using Domain.Models;
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
        public Employee Employee { get; set; }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set { SetProperty(ref _secondName, value); }
        }
        private DateOnly dateOfBirth;
        public DateOnly DateOfBirth
        {
            get { return dateOfBirth; }
            set { SetProperty(ref dateOfBirth, value); }
        }
        private DateOnly employmentDate;
        public DateOnly EmploymentDate
        {
            get { return employmentDate; }
            set { SetProperty(ref employmentDate, value); }
        }
        private string _post;
        public string Post
        {
            get { return _post; }
            set { SetProperty(ref _post, value); }
        }
        private double _salary;
        public double Salary
        {
            get { return _salary; }
            set { SetProperty(ref _salary, value); }
        }
    }
}
