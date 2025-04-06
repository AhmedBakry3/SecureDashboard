

using AutoMapper;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _Mapper) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _Mapper.Map<CreatedEmployeeDto , Employee>(employeeDto);
            return _employeeRepository.Add(Employee);
        }
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(EmployeeSearchName))
                employees = _employeeRepository.GetAll();
            else
                employees = _employeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var EmployeeDto = _Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeByID(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            return Employee is null ? null : _Mapper.Map<Employee, EmployeeDetailsDto>(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var Employee = _Mapper.Map<UpdatedEmployeeDto , Employee>(employeeDto);
            return _employeeRepository.Update(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            if(Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                return _employeeRepository.Update(Employee) > 0 ? true : false;
            }
        }
    }
}
