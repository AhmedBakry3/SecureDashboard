
namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateOnly DateOfCreation { get; set; }
    }
}
