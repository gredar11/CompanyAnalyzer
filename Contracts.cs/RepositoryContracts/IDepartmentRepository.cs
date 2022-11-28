using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.cs.RepositoryContracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments(Guid companyId, bool trackChanges);
    }
}
