using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Dtos
{
    public class DepartmentDto
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [ForeignKey("Head")]
        public Guid? HeadEmployeeId { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
