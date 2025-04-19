using Demo.DataAccess.Models.RoleManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.RoleDto
{
    public class UpdatedRoleDto
    {
        public string Id { get; set; }
        public RoleName RoleName { get; set; }
    }
}
