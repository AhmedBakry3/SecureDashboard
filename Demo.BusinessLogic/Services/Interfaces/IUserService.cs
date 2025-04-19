using Demo.BusinessLogic.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        bool DeleteUser(string id);
        IEnumerable<UserDto> GetAllUsers(string? UserSearchName);
        UserDetailsDto? GetUserByID(string id);
        int UpdateUser(UpdatedUserDto UserDto);
    }
}
