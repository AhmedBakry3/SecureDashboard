


namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDto
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required !!!!!!")]
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        [Required]
        [Range(100, int.MaxValue)]
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
    }
}
