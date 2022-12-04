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
    public class EmployeeService
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public EmployeeService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task CreateEmployeeAsync(EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            _repositoryManager.EmployeeRepository.CreateEmployee(entity);
            await _repositoryManager.SaveAsync();
        }
        public void DeleteEmployee(Guid id)
        {
            _repositoryManager.EmployeeRepository.DeleteEmployee(id);
            _repositoryManager.SaveChanges();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesByCompany(Guid companyId, Guid departmentId, bool trackChanges)
        {
            var entities = await _repositoryManager.EmployeeRepository.GetAllEmployeesByCompany(companyId, departmentId, trackChanges);

            return _mapper.Map<IEnumerable<EmployeeDto>>(entities);
        }

        public async Task<EmployeeDto> GetEmployeeById(Guid companyId, Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var entity = await _repositoryManager.EmployeeRepository.GetEmployeeById(companyId, departmentId, employeeId, trackChanges);
            return _mapper.Map<EmployeeDto>(entity);
        }

        public async Task UpdateEmployee(EmployeeDto employee)
        {
            var entity = _repositoryManager.EmployeeRepository.GetEmployeeById(employee.EmployeeId, true).Result;
            _mapper.Map(employee, entity);
            //_repositoryManager.EmployeeRepository.UpdateEmployee(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
