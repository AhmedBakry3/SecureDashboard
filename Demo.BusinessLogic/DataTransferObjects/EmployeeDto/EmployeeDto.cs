
namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
