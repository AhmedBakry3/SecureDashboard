
namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int CreateEmployee(CreateEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeByID(int id);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
    }
}
