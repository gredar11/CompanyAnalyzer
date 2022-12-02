using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Dtos
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly EmploymentDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public string Post { get; set; }
        public double Salary { get; set; }
    }
}
