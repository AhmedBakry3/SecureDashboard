


namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDto
{
    public class CreatedDepartmentDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
    }
}
