using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos
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
