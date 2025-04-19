using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.UserDto
{
    public class UpdatedUserDto
    {
        public string Id { get; set; }
        public string Fname { get; set; } = default!;
        public string Lname { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }
}
