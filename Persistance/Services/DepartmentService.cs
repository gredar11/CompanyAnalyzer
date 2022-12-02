using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistance.Services
{
    public class DepartmentService
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public DepartmentService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public void CreateDepartment(DepartmentDto departmentDto)
        {
            var entity = _mapper.Map<Department>(departmentDto);
            _repositoryManager.DepartmentRepository.CreateDepartment(entity);
            _repositoryManager.SaveAsync();
        }

        public async Task DeleteDepartment(Guid id)
        {
            await _repositoryManager.DepartmentRepository.DeleteDepartment(id);
            await _repositoryManager.SaveAsync(); 
        }

        public async Task<DepartmentDto> GetDepartmentById(Guid companyId, Guid departmentId, bool trackChanges)
        {
            var result = await _repositoryManager.DepartmentRepository.GetDepartmentById(companyId, departmentId, trackChanges);

            return _mapper.Map<DepartmentDto>(result);
        }
        public async Task<DepartmentDto> GetDepartmentById(Guid departmentId, bool trackChanges)
        {
            var result = await _repositoryManager.DepartmentRepository.GetDepartmentById(   departmentId, trackChanges);

            return _mapper.Map<DepartmentDto>(result);
        }
        public async Task<IEnumerable<DepartmentDto>> GetDepartments(Guid companyId, bool trackChanges)
        {
            var result = await _repositoryManager.DepartmentRepository.GetDepartments(companyId, trackChanges);

            return _mapper.Map<IEnumerable<DepartmentDto>>(result);
        }

        public void UpdateDepartment(DepartmentDto department)
        {
            var entity = _repositoryManager.DepartmentRepository.GetDepartmentById(department.CompanyId.Value, department.DepartmentId, true).Result;
            _mapper.Map(department, entity);
            _repositoryManager.DepartmentRepository.UpdateDepartment(entity);
            _repositoryManager.SaveAsync();
        }
    }
}
