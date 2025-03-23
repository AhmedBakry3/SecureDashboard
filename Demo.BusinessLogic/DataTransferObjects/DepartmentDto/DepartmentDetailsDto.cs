
namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDto
{

    public class DepartmentDetailsDto
    {
        //Constructor -- Based Mapping
        //public DepartmentDetailsDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    Code = department.Code;
        //    Description = department.Description;
        //    IsDeleted = department.IsDeleted;
        //    CreatedBy = department.CreatedBy;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
        //    LastModifiedBy = department.LastModifiedBy;
        //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn);
        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }





    }
}
