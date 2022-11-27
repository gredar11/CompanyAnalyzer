using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid HeadId { get; set; }
        public Employee Head { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
