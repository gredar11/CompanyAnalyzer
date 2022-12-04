using Contracts.cs.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyEmployeesDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Guid id)
        {
            var company = RepositoryContext.Companies.Find(id);
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAll(bool trackChanges)
        {
            var companies = await FindAll(trackChanges).ToListAsync();
            return companies;
        }

        public async Task<Company> GetCompany(Guid id, bool trackChanges)
        {
            var company = await FindByCondition(c => c.CompanyId == id, trackChanges).SingleOrDefaultAsync();
            return company;
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }
    }
}
