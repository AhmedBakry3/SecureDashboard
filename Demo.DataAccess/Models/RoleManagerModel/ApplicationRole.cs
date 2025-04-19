using Demo.DataAccess.Models.UserManagerModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Models.RoleManagerModel
{
    public class ApplicationRole : IdentityRole
    {
        public RoleName RoleName { get; set; }
        public virtual ICollection<UserManager> userManagers { get; set; } = new HashSet<UserManager>();
    }
}
