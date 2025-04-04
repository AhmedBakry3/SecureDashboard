
namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        //Create Employee
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Add(employeeDto.ToEntity());  
        }

        //Delete Employee
        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            if (Employee is null) return false;
            else
            {
                int Result = _employeeRepository.Remove(Employee);
                return Result > 0 ? true : false;  
            }
        }

        //Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = _employeeRepository.GetAll();
            return Employees.Select(E=>E.ToEmployeesDto());
        }

        //Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeByID(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            return Employee is null ? null : Employee.ToEmployeeDetailsDto();
        }

        //Update Employee
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(employeeDto.ToEntity());

        }
    }
}
