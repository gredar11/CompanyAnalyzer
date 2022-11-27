using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string Adress { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
