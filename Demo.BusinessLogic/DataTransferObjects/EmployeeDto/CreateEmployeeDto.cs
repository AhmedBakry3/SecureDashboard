

namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } =string.Empty; 
        public int Age { get; set; }

        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
