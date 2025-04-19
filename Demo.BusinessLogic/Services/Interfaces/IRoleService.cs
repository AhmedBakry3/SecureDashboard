using Demo.BusinessLogic.DataTransferObjects.RoleDto;
using Demo.BusinessLogic.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IRoleService
    {
        int CreateRole(CreatedRoleDto roleDto);
        bool DeleteRole(string id);
        IEnumerable<RoleDto> GetAllRoles(string? RoleSearchName);
        RoleDetailsDto? GetRoleByID(string id);
        int UpdateRole(UpdatedRoleDto RoleDto);
    }
}
