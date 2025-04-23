using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.RoleDto;
using Demo.BusinessLogic.DataTransferObjects.UserDto;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Models.UserManagerModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region EmployeeModel Mapping

            CreateMap<Employee, EmployeeDto>()
                  .ForMember(Dest => Dest.EmpGender, Options => Options.MapFrom(Src => Src.Gender))
                  .ForMember(Dest => Dest.EmpType, Options => Options.MapFrom(Src => Src.EmployeeType))
                  .ForMember(Dest => Dest.Department, Options => Options.MapFrom(Src => Src.Department != null ? Src.Department.Name : null));

            CreateMap<Employee, EmployeeDetailsDto>()
                  .ForMember(Dest => Dest.Gender, Options => Options.MapFrom(Src => Src.Gender))
                  .ForMember(Dest => Dest.EmployeeType, Options => Options.MapFrom(Src => Src.EmployeeType))
                  .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => DateOnly.FromDateTime(Src.HiringDate)))
                  .ForMember(Dest => Dest.Department, Options => Options.MapFrom(Src => Src.Department != null ? Src.Department.Name : null))
                  .ForMember(Dest => Dest.Image, Options => Options.MapFrom(Src => Src.ImageName));


            CreateMap<CreatedEmployeeDto, Employee>()
                  .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                   .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue))); 
    #endregion

            #region UserModel Mapping
            CreateMap<UserManager, UserDto>()
                   .ForMember(Dest => Dest.Fname, Options => Options.MapFrom(src => src.FirstName))
                   .ForMember(Dest => Dest.Lname, Options => Options.MapFrom(src => src.LastName))
                   .ForMember(Dest => Dest.Roles, Options => Options.MapFrom(src => src.Roles != null ? src.Roles.RoleName.ToString() : null));

            CreateMap<UserManager, UserDetailsDto>()
                   .ForMember(Dest => Dest.Fname, opt => opt.MapFrom(src => src.FirstName))
                   .ForMember(Dest => Dest.Lname, opt => opt.MapFrom(src => src.LastName));


            CreateMap<UpdatedUserDto, UserManager>()
                   .ForMember(Dest => Dest.FirstName, opt => opt.MapFrom(src => src.Fname))
                   .ForMember(Dest => Dest.LastName, opt => opt.MapFrom(src => src.Lname)); 
            #endregion

            #region RoleModel Mapping

            CreateMap<ApplicationRole, RoleDto>()
                  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ApplicationRole, RoleDetailsDto>()
                  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreatedRoleDto, ApplicationRole>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<UpdatedRoleDto, ApplicationRole>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)); 
            #endregion

        }
    }
}
