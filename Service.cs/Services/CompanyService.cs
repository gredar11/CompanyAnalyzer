using AutoMapper;
using Domain.Models;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public class CompanyService
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CompanyService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task CreateCompanyAsync(CompanyDto company)
        {
            var entity = _mapper.Map<Company>(company);  
            _repositoryManager.CompanyRepository.CreateCompany(entity);
            await _repositoryManager.SaveAsync();
        }

        public void DeleteCompany(Guid id)
        {
            _repositoryManager.CompanyRepository.DeleteCompany(id);
            _repositoryManager.SaveChanges();
        }

        public async Task<IEnumerable<CompanyDto>> GetAll(bool trackChanges)
        {
            var companies = await _repositoryManager.CompanyRepository.GetAll(trackChanges);
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompany(Guid id, bool trackChanges)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompany(id, false);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task UpdateCompany(CompanyDto company)
        {
            var entity = _repositoryManager.CompanyRepository.GetCompany(company.CompanyId, true).Result;
            _mapper.Map(company, entity);
            //_repositoryManager.CompanyRepository.UpdateCompany(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
