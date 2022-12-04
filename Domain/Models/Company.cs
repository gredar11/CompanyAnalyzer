using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Company
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public DateOnly EstablishmentDate { get; set; }
        [Required]
        public string Adress { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
