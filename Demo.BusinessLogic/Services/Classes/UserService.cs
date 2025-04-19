using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.UserDto;
using Demo.DataAccess.Models.UserManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class UserService(IUnitOfWork _unitOfWork , IMapper _Mapper) : IUserService
    {
        public IEnumerable<UserDto> GetAllUsers(string? UserSearchName)
        {
            IEnumerable<UserManager> Users;
            if (string.IsNullOrEmpty(UserSearchName))
                Users = _unitOfWork.UserManagerRepository.GetAll();
            else
                Users = _unitOfWork.UserManagerRepository.GetAll(U => U.FirstName.ToLower().Contains(UserSearchName.ToLower()));
            var UserDto = _Mapper.Map<IEnumerable<UserManager>, IEnumerable<UserDto>>(Users);
            return UserDto;
        }

        public UserDetailsDto? GetUserByID(string id)
        {
            var User = _unitOfWork.UserManagerRepository.GetById(id);
            return User is null ? null : _Mapper.Map<UserManager, UserDetailsDto>(User);
        }

        public int UpdateUser(UpdatedUserDto UserDto)
        {
            var existingUser = _unitOfWork.UserManagerRepository.GetById(UserDto.Id);

            if (existingUser == null)
                return 0;

            _Mapper.Map(UserDto, existingUser); 

            _unitOfWork.UserManagerRepository.Update(existingUser);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteUser(string id)
        {
            var User = _unitOfWork.UserManagerRepository.GetById(id);
            if (User is not null)
                _unitOfWork.UserManagerRepository.Remove(User);
            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }
    }
}
