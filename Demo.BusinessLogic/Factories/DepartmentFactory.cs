using Demo.BusinessLogic.DataTransferObjects;
using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
     static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                IsDeleted = department.IsDeleted,
                CreatedBy = department.CreatedBy,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),

            };
        }
    }
}
