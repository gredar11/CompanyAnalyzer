using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.cs.RepositoryContracts
{
    public interface ICompanyRepository
    {
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        Task<IEnumerable<Company>> GetAll(bool trackChanges);
        Task<Company> GetCompany(Guid id, bool trackChanges);
        void DeleteCompany(Guid id);
    }
}
