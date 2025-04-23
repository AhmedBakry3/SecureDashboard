using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.UserDto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Fname { get; set; } = default!;
        public string Lname { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; }
        [Phone]  
        public string? PhoneNumber { get; set; } 
        public string? Roles { get; set; }
    }
}
