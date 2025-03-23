
namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        //Get Department By ID
        //Manual Mapping
        //Auto Mapper
        //Constructor Mapping
        //Extenstion Methods
        public DepartmentDetailsDto? GetDepartmentByID(int id)
        {

            var Department = _departmentRepository.GetById(id);

            return Department is null ? null : Department.ToDepartmentDetailsDto();
        }

        //Add Department 
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var Department = departmentDto.ToEntity();
            return _departmentRepository.Add(Department);
        }
        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);

            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.Remove(Department);
                return Result > 0 ? true : false;
            }

        }
    }
}
