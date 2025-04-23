using Microsoft.Extensions.Primitives;

namespace Demo.Presentation.ViewModels.UserViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Fname { get; set; } = default!;
        public string Lname { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }
}
