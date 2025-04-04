

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
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking)
        {
            var EmployeeDto = _employeeRepository.GetAll(E=> new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Salary = E.Salary,
                Age = E.Age,    
            }).Where(E=>E.Age >20);
            //var Employees = _employeeRepository.GetAll(WithTracking);
            //var EmployeeDto = _Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
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
