using AutoMapper;
using Domain.Models;
using Service.Dtos;

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
        public async Task CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var entity = _mapper.Map<Department>(departmentDto);
            _repositoryManager.DepartmentRepository.CreateDepartment(entity);
            await _repositoryManager.SaveAsync();
        }

        public void DeleteDepartment(Guid id)
        {
            _repositoryManager.DepartmentRepository.DeleteDepartment(id);
            _repositoryManager.SaveChanges(); 
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

        public async Task UpdateDepartmentAsync(DepartmentDto department)
        {
            var entity = _repositoryManager.DepartmentRepository.GetDepartmentById(department.CompanyId.Value, department.DepartmentId, true).Result;
            _mapper.Map(department, entity);
            //_repositoryManager.DepartmentRepository.UpdateDepartment(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
