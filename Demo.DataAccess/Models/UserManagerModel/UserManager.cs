using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.RoleManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Models.UserManagerModel
{
    public class UserManager : ApplicationUser
    {
        public string? RolesId { get; set; }
        public virtual ApplicationRole Roles { get; set; }
    }
}
