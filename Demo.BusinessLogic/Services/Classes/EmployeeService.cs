

using AutoMapper;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork , IMapper _Mapper) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _Mapper.Map<CreatedEmployeeDto , Employee>(employeeDto);
             _unitOfWork.EmployeeRepository.Add(Employee); //Add Locally
            //insert
            //Update
            //Delete
            return _unitOfWork.SaveChanges();
        }
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var EmployeeDto = _Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeByID(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            return Employee is null ? null : _Mapper.Map<Employee, EmployeeDetailsDto>(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var Employee = _Mapper.Map<UpdatedEmployeeDto , Employee>(employeeDto);
             _unitOfWork.EmployeeRepository.Update(Employee); //Add Locally
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            if(Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(Employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
