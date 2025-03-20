using Demo.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services
{
    internal class DepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) // 1.Injection
        {

        }
    }
}
