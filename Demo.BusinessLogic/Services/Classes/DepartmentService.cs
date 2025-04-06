
namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {

        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        //Get Department By ID
        //Manual Mapping
        //Auto Mapper
        //Constructor Mapping
        //Extenstion Methods
        public DepartmentDetailsDto? GetDepartmentByID(int id)
        {

            var Department = _unitOfWork.DepartmentRepository.GetById(id);

            return Department is null ? null : Department.ToDepartmentDetailsDto();
        }

        //Add Department 
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var Department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(Department);
            return _unitOfWork.SaveChanges();
        }
        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);

            if (Department is null) return false;
            else
            {
                 _unitOfWork.DepartmentRepository.Remove(Department);
                 return _unitOfWork.SaveChanges() > 0 ? true : false;
            }

        }
    }
}
