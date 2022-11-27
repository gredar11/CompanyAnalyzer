using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [ForeignKey("Head")]
        public Guid? HeadEmployeeId { get; set; }
        public Employee? Head { get; set; }
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
