using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.DataTransferObjects.RoleDto;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.RoleManagerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class RoleService(IUnitOfWork _unitOfWork , IMapper _Mapper) : IRoleService
    {
        public IEnumerable<RoleDto> GetAllRoles(string? RoleSearchName)
        {

            var roles = _unitOfWork.roleManagerRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(RoleSearchName))
            {
                var lowerSearch = RoleSearchName.Trim().ToLower();
                roles = roles.Where(r =>
                    r.RoleName
                     .ToString()       
                     .ToLower()
                     .Contains(lowerSearch));
            }
            return _Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleDto>>(roles);
        }

        public RoleDetailsDto? GetRoleByID(string id)
        {
            var Role = _unitOfWork.roleManagerRepository.GetById(id);
            return Role is null ? null : _Mapper.Map<ApplicationRole, RoleDetailsDto>(Role);
        }
        public int CreateRole(CreatedRoleDto roleDto)
        {
            var Role = _Mapper.Map<CreatedRoleDto, ApplicationRole>(roleDto);
            if(Role is not null)
            _unitOfWork.roleManagerRepository.Add(Role);
            return _unitOfWork.SaveChanges();

        }
        public int UpdateRole(UpdatedRoleDto RoleDto)
        {
            var Role = _Mapper.Map<UpdatedRoleDto, ApplicationRole>(RoleDto);
            if (Role is not null)
                _unitOfWork.roleManagerRepository.Update(Role);
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteRole(string id)
        {
            var Role = _unitOfWork.roleManagerRepository.GetById(id);
            if (Role is not null)
                _unitOfWork.roleManagerRepository.Remove(Role);
            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }


    }
}
