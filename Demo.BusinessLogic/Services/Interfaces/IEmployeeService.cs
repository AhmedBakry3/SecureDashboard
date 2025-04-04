
namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking );
        EmployeeDetailsDto? GetEmployeeByID(int id);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
    }
}
