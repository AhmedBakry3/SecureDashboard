using Demo.DataAccess.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories
{
    internal class DepartmentRepository(ApplicationDbContext dbContext)
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //CURD Operations
        //Get All
        //Get By Id
        public Department? GetById (int id )
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }
        //Update
        //Delete
        //Insert
    }
}
