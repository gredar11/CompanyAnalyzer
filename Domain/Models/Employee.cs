using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly EmploymentDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Required]
        public string Post { get; set; }
        public double Salary { get; set; }
    }
}
