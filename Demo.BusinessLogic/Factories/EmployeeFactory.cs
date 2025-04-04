
namespace Demo.BusinessLogic.Factories
{
    static class EmployeeFactory
    {
        //Get All Employees
        public static EmployeeDto ToEmployeesDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
                Id = employee.Id,
            };
        }

        //Get Employee By Id 
        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
                Id = employee.Id,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber
            };
        }
        //Create Employee
        public static Employee ToEntity(this CreatedEmployeeDto employeeDto)
        {
            return new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                IsActive = employeeDto.IsActive,
                HiringDate = employeeDto.HiringDate,
                PhoneNumber = employeeDto.PhoneNumber
               
            };
        
        }
        //Update Employee
        public static Employee ToEntity(this UpdatedEmployeeDto employeeDto)
        {
            return new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                IsActive = employeeDto.IsActive,
                HiringDate = employeeDto.HiringDate,
                PhoneNumber = employeeDto.PhoneNumber
            };

        }
    }
}
