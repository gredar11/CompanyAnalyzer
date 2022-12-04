using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Domain.Models;
using CompanyAnalyzerWpf.ViewModels.ReportDialogs;

namespace CompanyAnalyzerWpf.Tools
{
    public class TotalSalaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
                return DependencyProperty.UnsetValue;
            var type = value.GetType();
            var collection = (ReadOnlyObservableCollection<object>)value;
            if (collection.Count() < 1) return 0.0;
            if (collection[0] is not EmployeeSalaryViewModel)
            {
                List<EmployeeSalaryViewModel> employeeSalaryViewModels = new List<EmployeeSalaryViewModel>();
                foreach (var collectionItem in collection)
                {
                    var group = ((CollectionViewGroup)collectionItem);
                    employeeSalaryViewModels.AddRange(group.Items.Cast<EmployeeSalaryViewModel>());
                }
                return SumTotalSalary(employeeSalaryViewModels);
            }
            var updateEntries = collection.Cast<EmployeeSalaryViewModel>();
            return SumTotalSalary(updateEntries);
            // TODO Return number of references
        }

        private static object SumTotalSalary(IEnumerable<EmployeeSalaryViewModel> updateEntries)
        {
            double sum = 0.0;
            foreach (var emp in updateEntries)
            {
                sum += emp.Salary;
            }
            return sum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        
    }
}
