
namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDto
{
    public class EmployeeDetailsDto
    {
        //Return Id[PK], Name, Age, Address, Is Active, Salary , Email , Phone Number, HiringDate Gender and EmployeeType.
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
